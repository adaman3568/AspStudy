using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspStudy.Data;
using AspStudy.Models;

namespace AspStudy.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private AspStudyContext db = new AspStudyContext();

        readonly CustomMemberShipProvider memberShip = new CustomMemberShipProvider();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            this.SetRoles();
            return View();
        }

        // POST: Users/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,Password,RoleIds")] User user)
        {
            // 登録するRolesを作成するが、ユーザー選択のRolesがDBのロールにあるか確認してから登録する。
            var roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // パスワードをHash化する。
                user.Password = memberShip.GeneratePasswordHash(user.UserName, user.Password);
                user.roles = roles;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // 処理完了後に戻すとき、選択されたRolesを再度送り選択状態にする。
            this.SetRoles(roles);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            this.SetRoles(user.roles);
            return View(user);
        }

        // POST: Users/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserName,Password,RoleIds")] User user)
        {
            // 選択されたロールがDBに存在するかチェック。
            var roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // DBから今回Edit対象のUserを取得する
                var dbUser = db.Users.Find(user.id);

                if (dbUser == null)
                {
                    return HttpNotFound();
                }

                dbUser.UserName = user.UserName;

                // 編集時に時にパスワードが変更されている場合、Hash化して登録する。
                if (!dbUser.Password.Equals(user.Password))
                {
                    dbUser.Password = memberShip.GeneratePasswordHash(user.UserName, user.Password);
                }

                dbUser.roles.Clear();
                //上記で取得したRolesをdbUserにRoleを登録
                roles.ForEach(role => dbUser.roles.Add(role));

                // ここを受け取ったuserで流してしまうと重複エラーになってしまう。
                db.Entry(dbUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            this.SetRoles(roles);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SetRoles(ICollection<Role> userRoles)
        {
            // RoleのICollectionのIDを取得し、配列にする。
            var roles = userRoles.Select(item => item.Id).ToArray();
            var list = db.Roles.Select(item => new SelectListItem
            {
                Text = item.RoleName,
                Value = item.Id.ToString(),
                //引数で入ってきたロールリストのIdが新たに入れようとしているRoleにあった場合、選択状態にする。
                Selected = roles.Contains(item.Id)
            }).ToList();
            // 最後はToListした方が安パイらしい


            // CreateとEditでこれらを受け取り、ListBoxに投げる。
            ViewBag.RoleIds = list;
        }
        /// <summary>
        /// 新規作成の場合選択状態にさせたくないので、引数無しバージョンを作成しておく。
        /// </summary>
        private void SetRoles()
        {
            SetRoles(new List<Role>());
        }
    }
}
