using CodeHub.Filters;
using CodeHub.Models;
using CodeHub.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodeHub.Controllers
{
    [Authentication]
    public class APIController : Controller
    {
        private Connection con;
        private UploadService service;

        public APIController()
        {
            con = new Connection();
            service = new UploadService();
        }
        public JsonResult GetUser()
        {
            try
            {
                if (Session["User"] != null && Session["User"] is Manager)
                {
                    Manager manager = (Manager)Session["User"];
                    return Json(new { success = true, name = manager.FirstName + " " + manager.LastName });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }
        public JsonResult createType(String Name)
        {
            try
            {
                if (!Name.Equals(""))
                {
                    Models.Type type = new Models.Type
                    {
                        Name = Name,
                    };
                    con.Types.Add(type);
                    con.SaveChanges();
                    return Json(new { success = true, type });
                }
                else return Json(new { success = false, msg = "Hãy nhập thể loại" });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult editType(int ID, String Name)
        {
            try
            {
                var type = con.Types.SingleOrDefault(x => x.ID == ID);
                if (type != null)
                {
                    if (!Name.Equals(""))
                    {
                        type.Name = Name;
                        con.SaveChanges();
                        return Json(new { success = true });
                    }
                    else return Json(new { success = false, msg = "Hãy nhập thể loại" });
                }
                else return Json(new { success = false, msg = "Không tìm thấy thể loại" });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getType(int ID)
        {
            try
            {
                var type = con.Types.Select(x => new { x.ID, x.Name }).SingleOrDefault(x => x.ID == ID);
                if (type != null) return Json(new { success = true, type });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getTypes(String search, int page)
        {
            try
            {
                if (page != 0)
                {
                    var numPage = 0;
                    int pageSize = 3;
                    var getList = con.Types.Select(x => new { x.ID, x.Name }).Where(x => x.Name.Contains(search)).ToList();
                    numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                    getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new { success = true, list = getList, numPage });
                }
                else
                {
                    return Json(new { success = true, list = con.Types.Select(x => new { x.ID, x.Name }) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getLanguages(String search, int page)
        {
            try
            {

                if (page != 0)
                {
                    var numPage = 0;
                    int pageSize = 3;
                    var getList = con.Languages.Select(x => new { x.ID, x.Name }).Where(x => x.Name.Contains(search)).ToList();
                    numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                    getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    return Json(new { success = true, list = getList, numPage });
                }
                else
                {
                    return Json(new { success = true, list = con.Languages.Select(x => new { x.ID, x.Name }).ToList() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult createLanguage(String Name)
        {
            try
            {
                if (!Name.Equals(""))
                {
                    Language language = new Language
                    {
                        Name = Name,
                    };
                    con.Languages.Add(language);
                    con.SaveChanges();
                    return Json(new { success = true, language });
                }
                else return Json(new { success = false, msg = "Hãy nhập ngôn ngữ" });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult editLanguage(int ID, String Name)
        {
            try
            {
                var language = con.Languages.SingleOrDefault(x => x.ID == ID);
                if (language != null)
                {
                    if (!Name.Equals(""))
                    {
                        language.Name = Name;
                        con.SaveChanges();
                        return Json(new { success = true });
                    }
                    else return Json(new { success = false, msg = "Hãy nhập ngôn ngữ" });
                }
                else return Json(new { success = false, msg = "Không tìm thấy ngôn ngữ" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getLanguage(int ID)
        {
            try
            {
                var language = con.Languages.Select(x => new { x.ID, x.Name }).SingleOrDefault(x => x.ID == ID);
                if (language != null) return Json(new { success = true, language });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getRequest(int id)
        {
            try
            {
                var request = con.Requests.Select(x =>
                new
                {
                    x.ID,
                    x.Name,
                    x.Description,
                    x.CreatedDate,
                    x.Status
                }).SingleOrDefault(x => x.ID == id);
                if (request != null) return Json(new { success = true, request });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult updateStatusRequest(int id, int status)
        {
            try
            {
                var request = con.Requests.SingleOrDefault(x => x.ID == id);
                if (request != null)
                {
                    if (status == 0) request.Status = -1;
                    else if (status == 1)
                    {
                        if (request.Status > -1 && request.Status <= 2)
                        {
                            request.Status++;
                        }
                    }
                    con.SaveChanges();
                    return Json(new { success = true, status = request.Status });
                }
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getRequests(String search, int page, String start, String end)
        {
            try
            {
                DateTime? startDate = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : (DateTime?)null;
                DateTime? endDate = !string.IsNullOrEmpty(end) ? DateTime.Parse(end).AddDays(1).Date : (DateTime?)null;
                var numPage = 0;
                int pageSize = 3;
                var getList = con.Requests.Select(x => new
                {
                    x.ID,
                    x.User.Username,
                    Infor = x.User.FirstName + " " + x.User.LastName + " (" + x.User.Email + ")",
                    x.Name,
                    Date = x.CreatedDate,
                    x.Status
                }).ToList();
                if (startDate != null && endDate != null)
                {
                    getList = getList.Where(x =>
                        (x.Date >= startDate && x.Date <= endDate) &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Name.Contains(search)
                        )).ToList();
                }
                else if (startDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date >= startDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Name.Contains(search)
                        )).ToList();
                }
                else if (endDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date <= endDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Name.Contains(search)
                        )).ToList();
                }
                else
                {
                    getList = getList.Where(x =>
                    x.Username.Contains(search) ||
                    x.Infor.Contains(search) ||
                    x.Name.Contains(search)
                    ).ToList();
                }
                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult getCustomers(String search, int page)
        {
            try
            {
                var numPage = 0;
                int pageSize = 3;
                var getList = con.Users.Select(x => new
                {
                    x.ID,
                    x.Username,
                    FullName = x.FirstName + " " + x.LastName,
                    Sex = x.Gender == true ? "Nam" : "Nữ",
                    x.DateOfBirth,
                    x.Email,
                    Coin = x.Currency,
                    x.IsActive
                }).Where(x => x.Username.Contains(search) || x.FullName.Contains(search)).ToList();


                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult getReports(String search, int page, String start, String end)
        {
            try
            {
                DateTime? startDate = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : (DateTime?)null;
                DateTime? endDate = !string.IsNullOrEmpty(end) ? DateTime.Parse(end).AddDays(1).Date : (DateTime?)null;
                var numPage = 0;
                int pageSize = 3;
                var getList = con.Reports.Select(x => new
                {
                    x.User.Username,
                    Infor = x.User.FirstName + " " + x.User.LastName + " - " + x.User.Email,
                    Code = x.SourceCode.ID + ". " + x.SourceCode.Name + " (" + x.SourceCode.Language.Name + ")",
                    Report = x.ReportType.Name,
                    Date = x.CreatedDate
                }).ToList();

                if (startDate != null && endDate != null)
                {
                    getList = getList.Where(x =>
                        (x.Date >= startDate && x.Date <= endDate) &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search) ||
                        x.Report.Contains(search)
                        )).ToList();
                }
                else if (startDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date >= startDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search) ||
                        x.Report.Contains(search)
                        )).ToList();
                }
                else if (endDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date <= endDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search) ||
                        x.Report.Contains(search)
                        )).ToList();
                }
                else
                {
                    getList = getList.Where(x =>
                    x.Username.Contains(search) ||
                    x.Infor.Contains(search) ||
                    x.Code.Contains(search) ||
                    x.Report.Contains(search)
                    ).ToList();
                }
                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult getOrders(String search, int page, String start, String end)
        {
            try
            {
                DateTime? startDate = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : (DateTime?)null;
                DateTime? endDate = !string.IsNullOrEmpty(end) ? DateTime.Parse(end).AddDays(1).Date : (DateTime?)null;

                var numPage = 0;
                int pageSize = 3;
                var getList = con.Orders.Select(x => new
                {
                    x.User.Username,
                    Infor = x.User.FirstName + " " + x.User.LastName + " - " + x.User.Email,
                    Code = x.SourceCode.ID + ". " + x.SourceCode.Name + " (" + x.Fee + " xu" + ")",
                    Date = x.DateCreated
                }).ToList();

                if (startDate != null && endDate != null)
                {
                    getList = getList.Where(x =>
                        (x.Date >= startDate && x.Date <= endDate) &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search)
                        )).ToList();
                }
                else if (startDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date >= startDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search)
                        )).ToList();
                }
                else if (endDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date <= endDate &&
                        (x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Code.Contains(search)
                        )).ToList();
                }
                else
                {
                    getList = getList.Where(x =>
                    x.Username.Contains(search) ||
                    x.Infor.Contains(search) ||
                    x.Code.Contains(search)
                    ).ToList();
                }

                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult getDeposits(String search, int page, String start, String end)
        {
            try
            {
                DateTime? startDate = !string.IsNullOrEmpty(start) ? DateTime.Parse(start) : (DateTime?)null;
                DateTime? endDate = !string.IsNullOrEmpty(end) ? DateTime.Parse(end).AddDays(1).Date : (DateTime?)null;

                var numPage = 0;
                int pageSize = 3;
                var getList = con.DepositHistories.Select(x => new
                {
                    x.User.Username,
                    Infor = x.User.FirstName + " " + x.User.LastName + " - " + x.User.Email,
                    x.Amount,
                    Type = x.TransactionType,
                    x.Note,
                    Date = x.TransactionDate
                }).ToList();

                if (startDate != null && endDate != null)
                {
                    getList = getList.Where(x =>
                        (x.Date >= startDate && x.Date <= endDate) &&
                        (x.Username.Contains(search) || x.Infor.Contains(search) || x.Note.Contains(search))).ToList();
                }
                else if (startDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date >= startDate &&
                        (x.Username.Contains(search) || x.Infor.Contains(search) || x.Note.Contains(search))
                    ).ToList();
                }
                else if (endDate != null)
                {
                    getList = getList.Where(x =>
                        x.Date <= endDate &&
                        (x.Username.Contains(search) || x.Infor.Contains(search) || x.Note.Contains(search))
                    ).ToList();
                }
                else
                {
                    getList = getList.Where(x =>
                        x.Username.Contains(search) ||
                        x.Infor.Contains(search) ||
                        x.Note.Contains(search)).ToList();
                }

                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> UploadCode(int id, SourceCode src, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (string.IsNullOrEmpty(src.Name) ||
                string.IsNullOrEmpty(src.Description) ||
                string.IsNullOrEmpty(src.LinkVideo) ||
                string.IsNullOrEmpty(src.SourceLink) ||
                src.TypeID == 0 ||
                src.LanguageID == 0 ||
                src.Fee <= 0)
            {
                return Json(new { success = false, message = "Hãy nhập đầy đủ thông tin" });
            }

            if (Session["User"] == null)
            {
                return Json(new { success = false, message = "ID User null" });
            }

            Manager manager = (Manager)Session["User"];

            using (var transaction = con.Database.BeginTransaction())
            {
                try
                {
                    if (id == 0)
                    {
                        SourceCode createSource = new SourceCode
                        {
                            Name = src.Name,
                            TypeID = src.TypeID,
                            LanguageID = src.LanguageID,
                            Fee = src.Fee,
                            Description = src.Description,
                            LinkVideo = src.LinkVideo,
                            SourceLink = src.SourceLink,
                            IsDelete = false,
                            IsShow = true,
                            Coder = manager.ID
                        };
                        con.SourceCodes.Add(createSource);

                        foreach (var imageFile in imageFiles)
                        {
                            string imageUrl = await service.UploadImageAsync(imageFile);
                            if (string.IsNullOrEmpty(imageUrl))
                            {
                                transaction.Rollback();
                                return Json(new { success = false, message = "Chưa có ảnh nào được chọn" });
                            }
                            con.ImageUrls.Add(new ImageUrl { Source = createSource.ID, Url = imageUrl });
                        }
                        var detailCode = con.DetailCodes.SingleOrDefault(x => x.Source == createSource.ID);
                        if (detailCode == null)
                        {
                            con.DetailCodes.Add(
                                new DetailCode
                                {
                                    Source = createSource.ID,
                                    Views = 0,
                                    Purchases = 0
                                });
                        }
                    }
                    else
                    {
                        SourceCode updateSource = con.SourceCodes.SingleOrDefault(x => x.ID == id);
                        if (updateSource != null)
                        {
                            updateSource.Name = src.Name;
                            updateSource.TypeID = src.TypeID;
                            updateSource.LanguageID = src.LanguageID;
                            updateSource.Fee = src.Fee;
                            updateSource.LinkVideo = src.LinkVideo;
                            updateSource.SourceLink = src.SourceLink;
                            updateSource.Description = src.Description;

                            List<ImageUrl> images = new List<ImageUrl>();
                            foreach (var imageFile in imageFiles)
                            {
                                string imageUrl = await service.UploadImageAsync(imageFile);
                                if (!string.IsNullOrEmpty(imageUrl))
                                {
                                    images.Add(new ImageUrl { Source = updateSource.ID, Url = imageUrl });
                                }
                            }
                            if (images.Any())
                            {
                                con.ImageUrls.RemoveRange(con.ImageUrls.Where(x => x.Source == updateSource.ID).ToList());
                                con.ImageUrls.AddRange(images);
                            }
                        }
                    }
                    await con.SaveChangesAsync();
                    transaction.Commit();
                    return Json(new { success = true });
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, error = $"Đã xảy ra lỗi khi lưu dữ liệu. {ex}" });
                }
            }
        }

        public JsonResult getSources(String search, int type, int language, int page)
        {
            try
            {
                var numPage = 0;
                int pageSize = 3;
                var getList = con.SourceCodes
                    .Select(x => new { x.ID, x.Name, image = x.ImageUrls.Select(i => i.Url).FirstOrDefault(), x.LinkVideo, x.TypeID, TypeName = x.Type.Name, x.LanguageID, LanguageName = x.Language.Name, x.Fee, x.SourceLink, x.Manager.Username, x.IsShow, x.IsDelete })
                    .Where(x => x.Name.Contains(search) && x.IsDelete == false).ToList();

                if (type != 0)
                {
                    getList = getList.Where(x => x.TypeID == type).ToList();
                }
                if (language != 0)
                {
                    getList = getList.Where(x => x.LanguageID == language).ToList();
                }

                numPage = getList.Count() % pageSize == 0 ? getList.Count() / pageSize : getList.Count() / pageSize + 1;
                getList = getList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return Json(new { success = true, list = getList, numPage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult getSource(int id)
        {
            try
            {
                var source = con.SourceCodes.Select(x => new
                {
                    x.ID,
                    x.Name,
                    Type = x.TypeID,
                    Language = x.LanguageID,
                    images = x.ImageUrls.Select(i => new { i.Url }).ToList(),
                    x.LinkVideo,
                    x.Fee,
                    x.SourceLink,
                    x.Description,
                    x.IsDelete
                }).SingleOrDefault(x => x.ID == id && x.IsDelete == false);
                return Json(new { success = true, source });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public async Task<JsonResult> changeShow(int id)
        {
            try
            {
                var source = con.SourceCodes.SingleOrDefault(x => x.ID == id);
                if (source == null) return Json(new { success = false });
                source.IsShow = !source.IsShow;
                await con.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public async Task<JsonResult> handleActive(int id)
        {
            try
            {
                var user = con.Users.SingleOrDefault(x => x.ID == id);
                if (user == null) return Json(new { success = false });
                user.IsActive = !user.IsActive;
                await con.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public async Task<JsonResult> removeSource(int id)
        {
            try
            {
                var source = con.SourceCodes.SingleOrDefault(x => x.ID == id);
                if (source == null) return Json(new { success = false });
                source.IsDelete = true;
                await con.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public JsonResult totalOrder(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                return Json(new
                {
                    success = true,
                    count = con.Orders.Where(o => o.DateCreated >= startDate && o.DateCreated <= endDate).Count()
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult totalCoin(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                return Json(new
                {
                    success = true,
                    sum = con.DepositHistories
                    .Where(x => x.TransactionDate >= startDate &&
                    x.TransactionDate <= endDate &&
                    x.TransactionType == true).Sum(x => x.Amount)
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult totalDeposit(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                return Json(new
                {
                    success = true,
                    sum = con.DepositHistories.Where(x => x.TransactionDate >= startDate && x.TransactionDate <= endDate).Sum(x => x.Amount)
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult topOrder(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                var userWithMostOrders = con.Users
                    .GroupJoin(
                    con.Orders.Where(o => o.DateCreated >= startDate && o.DateCreated <= endDate),
                    user => user.ID,
                    order => order.UserID,
                    (user, orders)
                    => new
                    {
                        User = user,
                        OrderCount = orders.Count()
                    })
                    .OrderByDescending(u => u.OrderCount)
                    .Select(u => new
                    {
                        FullName = u.User.FirstName + " " + u.User.LastName,
                        Count = u.OrderCount
                    })
                    .FirstOrDefault();

                if (userWithMostOrders != null)
                    return Json(new { success = true, user = userWithMostOrders });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult topUpCoin(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                var userUpCoin = con.Users
                    .GroupJoin(
                    con.DepositHistories.Where(d =>
                    d.TransactionDate >= startDate &&
                    d.TransactionDate <= endDate &&
                    d.TransactionType == true),
                    user => user.ID,
                    deposit => deposit.UserID,
                    (user, deposit)
                    => new
                    {
                        User = user,
                        total = deposit.Sum(d => d.Amount)
                    })
                    .OrderByDescending(u => u.total)
                    .Select(u => new
                    {
                        FullName = u.User.FirstName + " " + u.User.LastName,
                        Total = u.total
                    })
                    .FirstOrDefault();

                if (userUpCoin != null)
                    return Json(new { success = true, user = userUpCoin });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult topCoin()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    users = con.Users
                    .OrderByDescending(u => u.Currency)
                    .Select(x => new { FullName = x.FirstName + " " + x.LastName, x.Currency })
                    .Take(3).ToList()
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult topCoder()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    coders = con.Managers
                    .OrderByDescending(m => m.SourceCodes.Count())
                    .Select(x => new { FullName = x.FirstName + " " + x.LastName, x.SourceCodes.Count })
                    .Take(3).ToList()
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
        public JsonResult topDeposit(int value)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                switch (value)
                {
                    case 0:
                        startDate = now.Date;
                        endDate = now.Date.AddDays(1).AddSeconds(-1);
                        break;
                    case 1:
                        startDate = new DateTime(now.Year, now.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case 2:
                        startDate = new DateTime(now.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    default:
                        break;
                }
                var userUpCoin = con.Users
                    .GroupJoin(
                    con.DepositHistories.Where(d => d.TransactionDate >= startDate && d.TransactionDate <= endDate),
                    user => user.ID,
                    deposit => deposit.UserID,
                    (user, deposit)
                    => new
                    {
                        User = user,
                        total = deposit.Sum(d => d.Amount)
                    })
                    .OrderByDescending(u => u.total)
                    .Select(u => new
                    {
                        FullName = u.User.FirstName + " " + u.User.LastName,
                        Total = u.total
                    })
                    .FirstOrDefault();

                if (userUpCoin != null)
                    return Json(new { success = true, user = userUpCoin });
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}