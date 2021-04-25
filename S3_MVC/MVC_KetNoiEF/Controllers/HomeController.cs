using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Bai5_KetNoiEF.Models;

namespace MVC_Bai5_KetNoiEF.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        QL_NhanSuEntities db = new QL_NhanSuEntities();
        public ActionResult Index()
        {
            return View();
        }
        //Hiển thị thông tin các phòng ban
        public ActionResult ShowDeparment(tbl_Deparment dept)
        {
            return View(db.tbl_Deparment.ToList());
        }
        //Thêm phòng ban
        public ActionResult CreateDept()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDept(tbl_Deparment dept) {
            if (ModelState.IsValid) {
                db.tbl_Deparment.Add(dept);
                db.SaveChanges();
                return RedirectToAction("ShowDeparment","Home");
            }
            return View(dept);
        }
        //Cập nhật phòng ban
        public ActionResult UpdateDept(int id) {
            tbl_Deparment dept = db.tbl_Deparment.Single(d => d.DeptId == id);
            if (dept == null)
                return HttpNotFound();
            return View();
        }
        [HttpPost]
        public ActionResult UpdateDept(tbl_Deparment dept)
        {
            if (ModelState.IsValid) {
                db.tbl_Deparment.Attach(dept);
                db.Entry(dept).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowDeparment", "Home");
            }
            return View();
        }
        //Hiển thị chi tiết phòng ban
        public ActionResult DetailDept(int id)
        {
            tbl_Deparment dept = db.tbl_Deparment.Single(d => d.DeptId == id);
            if (dept == null)
                return HttpNotFound();
            return View(dept);
        }
        //Hiển thị nhân viên
        public ActionResult ShowEmployee(tbl_Employee eml)
        {
            return View(db.tbl_Employee.ToList());
        }
        //
        public ActionResult CreateEmployee()
        {
            //Lấy dữ liệu cho dropdown List
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(tbl_Employee eml)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Employee.Add(eml);
                db.SaveChanges();
                return RedirectToAction("ShowEmployee", "Home");
            }
            return View(eml);
        }
        //Cập nhật Nhân viên
        public ActionResult UpdateEmployee(int id)
        {
            tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            if (emp == null)
                return HttpNotFound();
            return View(emp);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(tbl_Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Employee.Attach(emp);
                db.Entry(emp).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowEmployee", "Home");
            }
            return View();
        }
        //Xóa nhân viên
        public ActionResult DeleteEmployee(int id = 0) {
            tbl_Employee emp = db.tbl_Employee.Single(d=>d.Id == id);
            if (emp == null)
                return HttpNotFound();
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public ActionResult DeleteEmployeeConfirm(int id = 0)
        {
            //Lấy ra employee cần xóa
            tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
            if (emp == null)
                return HttpNotFound();
            db.Entry(emp).State=System.Data.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowEmployee","Home");
        }

        public ActionResult ShowEmplDepa() {
            var listED = db.tbl_Employee.Include("tbl_Deparment").ToList();
            return View(listED);
        }
        //Lấy dữ liệu phòng ban, truyền IdDept sang tbl_Deparment
        public ActionResult SideBarDept() {
            var dept = db.tbl_Deparment.ToList();
            return View(dept);
        }
        //Hiển các nhân viên theo từng mã phòng ban đầu vào
        public ActionResult ShowEmplParameter(int id) {
            var listE = db.tbl_Employee.Where(emp => emp.DeptId == id).ToList();
            return View(listE);
        }
        //Hiển thị chi tiết nhân viên
        public ActionResult DetailEmp(int id)
        {
            tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
            if (emp == null)
                return HttpNotFound();
            return View(emp);
        }
    }
}
