using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISpaceSRM.Models;
using APISpaceSRM;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Globalization;

namespace APISpaceSRM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class SpaceController : ControllerBase
    {
        DataContext db;
        public SpaceController(DataContext db)
        {
            this.db = db;

        }

        //Робота з послугами

        [HttpPost]
        [ActionName("AddService")]
        public async Task<ActionResult> AddService(Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest("Послуга null");
                }
                db.services.Add(
                    new Service
                    {
                        Name = service.Name,
                        Price = service.Price,
                        Procent = service.Procent,
                        Type = service.Type,
                    }
                    );
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{Id}")]
        [ActionName("DeleteService")]
        public ActionResult DeleteService(int Id)
        {
            if (Id >= 0)
            {
                Service? ser = db.services.FirstOrDefault(s => s.Id == Id);
                if (ser != null)
                {
                    db.services.Remove(ser);
                    db.SaveChanges();
                    return Ok("Знайдено");
                }
                return BadRequest("Не знайдено");
            }
            return BadRequest("неправельний формат id");
        }


        [HttpPost]
        [ActionName("EditService")]
        public async Task<ActionResult> EditService(Service service)
        {
            Service? thisService = await db.services.FirstOrDefaultAsync(s => service.Id == s.Id);
            if (thisService != null)
            {
                thisService.Price = service.Price;
                thisService.Name = service.Name;
                thisService.Type = service.Type;
                thisService.Procent = service.Procent;
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Не знайдено");
        }


        [HttpGet]
        [ActionName("GetServicesQuick")]
        public async Task<ActionResult<List<Service>>> GetServicesQuick()
        {
            try
            {
                List<Service> services = await db.services.ToListAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new List<Service>());
            }

        }

        [HttpGet]
        [ActionName("GetServices")]
        public async Task<ActionResult<List<Service>>> GetServices()
        {
            try
            {
                List<Service> services = await db.services.Include(u => u.Works).ThenInclude(o => o.Record)
                    .Include(m => m.Works).ThenInclude(a => a.Employers)
                    .ToListAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new List<Service>());
            }

        }


        //Робота з Клієнтом

        [HttpGet]
        [ActionName("GetClientsQuick")]
        public async Task<ActionResult<List<Client>>> GetClientsQuick()
        {
            try
            {
                List<Client> clients = await db.clients.ToListAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new List<Client>());
            }
        }

        [HttpGet]
        [ActionName("GetClients")]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            try
            {
                List<Client> clients = await db.clients.Include(u => u.Records).ThenInclude(m => m.Works).ThenInclude(v => v.Service).AsNoTracking()
                    .ToListAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new List<Client>());
            }
        }
        [HttpGet]
        [ActionName("GetWorks")]
        public async Task<ActionResult<List<Work>>> GetWorks()
        {
            try
            {
                List<Work> works = await db.works
                    .Include(u => u.Employers).AsNoTracking()
                    .Include(m => m.Record).AsNoTracking()
                    .Include(i => i.Service).AsNoTracking().ToListAsync();
                return Ok(works);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new List<Work>());
            }
        }

        [HttpPost]
        [ActionName("AddClient")]
        public async Task<ActionResult> AddClient(Client client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest();
                }
                db.clients.Add(
                    new Client
                    {
                        Name = client.Name,
                        SurName = client.SurName,
                        Phone = client.Phone,
                    });
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("EditClient")]
        public async Task<ActionResult> EditClient(Client client)
        {
            try
            {
                Client? thisClient = await db.clients.FirstOrDefaultAsync(s => client.Id == s.Id);
                if (thisClient != null)
                {

                    thisClient.Name = client.Name;
                    thisClient.SurName = client.SurName;
                    thisClient.Phone = client.Phone;
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("Не знайдено");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        [ActionName("DeleteClient")]
        public ActionResult DeleteClient(int Id)
        {
            try
            {
                if (Id >= 0)
                {
                    Client? ser = db.clients.FirstOrDefault(s => s.Id == Id);
                    if (ser != null)
                    {
                        db.clients.Remove(ser);
                        db.SaveChanges();
                        return Ok("Знайдено");
                    }
                    return BadRequest("Не знайдено");
                }
                return BadRequest("неправельний формат id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        ////робота з Набором послуг

        //[HttpGet]
        //[ActionName("GetSetServices")]
        //public async Task<ActionResult<List<SetService>>> GetSetServices()
        //{
        //    try
        //    {
        //        List<SetService> setServices = await db.setServices.Include(u => u.Services).ToListAsync();
        //        return Ok( setServices);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return BadRequest(new List<SetService>());
        //    }
        //}


        //[HttpPost]
        //[ActionName("AddSetService")]
        //public async Task<ActionResult> AddSetService(SetService setService)
        //{
        //    try
        //    {
        //        if (setService == null)
        //        {
        //            return BadRequest();
        //        }
        //        List<Service> services = new List<Service>();
        //        foreach (var i in setService.Services)
        //        {
        //            Service? e = db.services.FirstOrDefault(u => u.Id == i.Id);
        //            if (e != null)
        //            {
        //                services.Add(e);
        //            }
        //        }
        //        db.setServices.Add(
        //            new SetService
        //            {
        //                Name = setService.Name,
        //                Services = services,
        //                Discount = setService.Discount
        //            });
        //        await db.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost]
        //[ActionName("EditSetService")]
        //public async Task<ActionResult> EditSetService(SetService setService)
        //{
        //    try
        //    {
        //        SetService? thisSetService = await db.setServices.FirstOrDefaultAsync(s => setService.Id == s.Id);
        //        if (thisSetService != null)
        //        {

        //            thisSetService.Name = setService.Name;
        //            thisSetService.Discount = setService.Discount;
        //            await db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        return BadRequest("Не знайдено");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("{Id}")]
        //[ActionName("DeleteSetService")]
        //public ActionResult DeleteSetService(int Id)
        //{
        //    try
        //    {
        //        if (Id >= 0)
        //        {
        //            SetService? ser = db.setServices.FirstOrDefault(s => s.Id == Id);
        //            if (ser != null)
        //            {
        //                db.setServices.Remove(ser);
        //                db.SaveChanges();
        //                return Ok("Знайдено");
        //            }
        //            return BadRequest("Не знайдено");
        //        }
        //        return BadRequest("неправельний формат id");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //}

        //робота з Працівниками

        //Метод Quick не включає в себе звазки компонента
        [HttpGet]
        [ActionName("GetEmployersQuick")]
        public async Task<ActionResult<List<Employer>>> GetEmployersQuick()
        {
            try
            {
                List<Employer> employer = await db.employers.ToListAsync();
                return Ok(employer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new List<Employer>());
            }
        }
        [HttpGet]
        [ActionName("GetEmployers")]
        public async Task<ActionResult<List<Employer>>> GetEmployers()
        {
            try
            {
                List<Employer> employers = await db.employers.Include(u => u.Works).ThenInclude(i => i.Record).AsNoTracking()
                    .Include(p => p.Works).ThenInclude(m => m.Service).AsNoTracking().Include(k => k.Salaries).AsNoTracking()
                    .ToListAsync();
                foreach (Employer employer in employers)
                {
                    foreach (Work work in employer.Works) {
                        List<Employer> employersT = new List<Employer>();
                        Work? worksTrue = await db.works.Include(m => m.Employers).Where(i => i.Id == work.Id).FirstOrDefaultAsync();
                        if (worksTrue != null)
                        {
                            foreach (Employer empTrue in worksTrue.Employers)
                            {
                                employersT.Add(new Employer() {
                                    Name = empTrue.Name,
                                    SurName = empTrue.SurName,
                                    Phone = empTrue.Phone,
                                });
                            }
                        }
                        work.Employers = employersT;
                    }
                }
                return Ok(employers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new List<Employer>());
            }
        }

        [HttpPost]
        [ActionName("AddEmployer")]
        public async Task<ActionResult> AddEmployer(Employer employer)
        {
            try
            {
                if (employer == null)
                {
                    return BadRequest();
                }
                List<Work> works = new List<Work>();
                foreach (var i in employer.Works)
                {
                    Work? e = db.works.FirstOrDefault(u => u.Id == i.Id);
                    if (e != null)
                    {
                        works.Add(e);
                    }
                }
                db.employers.Add(
                    new Employer
                    {
                        Name = employer.Name,
                        SurName = employer.SurName,
                        Phone = employer.Phone,
                        Works = works,

                    });
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("EditEmployer")]
        public async Task<ActionResult> EditEmployer(Employer employer)
        {
            try
            {
                Employer? thisEmployer = await db.employers.FirstOrDefaultAsync(s => employer.Id == s.Id);
                if (thisEmployer != null)
                {

                    thisEmployer.Name = employer.Name;
                    thisEmployer.SurName = employer.SurName;
                    thisEmployer.Phone = employer.Phone;
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("Не знайдено");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        [ActionName("DeleteEmployer")]
        public ActionResult DeleteEmployer(int Id)
        {
            try
            {

                if (Id >= 0)
                {
                    Employer? ser = db.employers.FirstOrDefault(s => s.Id == Id);
                    if (ser != null)
                    {
                        db.employers.Remove(ser);
                        db.SaveChanges();
                        return Ok("Знайдено");
                    }
                    return BadRequest("Не знайдено");
                }
                return BadRequest("неправельний формат id");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }



        //Робота з записами

        //Додавання нового запису
        [HttpPost]
        [ActionName("AddRecord")]
        public async Task<ActionResult> AddRecord(Record record)
        {
            try
            {
                if (record != null)
                {
                    //Додавання запису
                    Client? client = db.clients.FirstOrDefault(s => s.Id == record.ClientId);
                    if (client == null)
                    {
                        return BadRequest("не знайдено клієнта");
                    }
                    if (record.Works == null || record.Works.Count == 0)
                    {
                        return BadRequest("Не додано робіт");
                    }
                    string messageStr = "";
                    // Встановити вміст запиту як XML
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;

                    if (currentTime < TimeSpan.FromHours(12))
                    {
                        messageStr = messageStr + "Доброго ранку";
                    }
                    else if (currentTime < TimeSpan.FromHours(18))
                    {
                        messageStr = messageStr + "Добрий день";
                    }
                    else
                    {
                        messageStr = messageStr + "Добрий вечір";
                    }

                    messageStr = $"{messageStr}, {client.Name},";
                    HttpClient httpClient = new HttpClient();
                    switch (record.Status)
                    {
                        case Status.Wait:
                            messageStr = $"{messageStr} ваше авто {record.Brand} записано на {record.DateStart.ToString("MM/dd/yyyy HH:mm")}";
                            break;
                        case Status.End:
                            messageStr = $"{messageStr} ваше авто {record.Brand} готове до видачі";
                            break;
                        case Status.Work:
                            messageStr = $"{messageStr} ваше авто {record.Brand} взято в роботу,очікуйте SMS про завершення роботи";
                            break;
                        case Status.Abolition:
                            messageStr = $"{messageStr} за вашим авто {record.Brand} відмова";
                            break;
                    }

                    string phoneNumber = client.Phone;
                    if (phoneNumber != "")
                    {
                        if (!phoneNumber.StartsWith("38"))
                        {
                            phoneNumber = "38" + phoneNumber;
                        }
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml("<request><auth><login>380977259326</login><password>Flyboxx125</password></auth>" +
                            $"<message><from>SpaceDetail</from><text>{messageStr}</text><recipient>{phoneNumber}</recipient></message></request>");
                        HttpContent httpContent = new StringContent(xmlDocument.OuterXml, Encoding.UTF8, "application/xml");


                        HttpResponseMessage response = await httpClient.PostAsync("https://api.letsads.com", httpContent);
                        string responseStr = await response.Content.ReadAsStringAsync();
                    }
                    Record thisRecord = new Record()
                    {
                        Client = client,
                        Brand = record.Brand,
                        NumberOfCar = record.NumberOfCar,
                        BodyType = record.BodyType,
                        BodySize = record.BodySize,

                        DateStart = record.DateStart,
                        DateEnd = record.DateEnd,
                        Discount = record.Discount,
                        Status = record.Status,
                        Sum = record.Sum,
                    };

                    db.records.Add(thisRecord);
                    await db.SaveChangesAsync();



                    //Додавання робіт запису
                    foreach (Work work in record.Works)
                    {
                        List<Employer> employers = new List<Employer>();
                        foreach (Employer i in work.Employers)
                        {
                            Employer? employer = db.employers.FirstOrDefault(u => u.Id == i.Id);
                            if (employer != null)
                            {
                                employers.Add(employer);
                            }
                        }
                        if (employers == null || employers.Count == 0)
                        {
                            return BadRequest("Введіть працівника/ів");
                        }
                        Service? service = db.services.FirstOrDefault(u => u.Id == work.Service.Id);
                        if (service == null)
                        {
                            return BadRequest("Послугу не було знайдено");
                        }


                        Work thisWork = new Work()
                        {
                            Employers = employers,
                            Service = service,
                            Record = thisRecord,
                            Price = work.Price,
                            TruePrice = work.Price,
                            DescriptionCost = work.DescriptionCost,
                            PriceCost = work.PriceCost,
                        };
                        db.works.Add(thisWork);
                    }
                    //Loading photo before start


                    await db.SaveChangesAsync();
                    return Ok(thisRecord.Id);


                }
                else
                {
                    return BadRequest("некоректний запис");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [ActionName("EditRecord")]
        public async Task<ActionResult> EditRecord(Record record)
        {
            try
            {
                Record? thisRecord = await db.records.Include(u => u.Works)
                    .Include(e => e.Client)
                    .Where(m => m.Id == record.Id).FirstOrDefaultAsync();

                Client? client = db.clients.FirstOrDefault(s => s.Id == record.ClientId);
                //Робота з смсками
                if (record.SendMessage)
                {

                    string messageStr = "";
                    // Встановити вміст запиту як XML
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;

                    if (currentTime < TimeSpan.FromHours(12))
                    {
                        messageStr = messageStr + "Доброго ранку";
                    }
                    else if (currentTime < TimeSpan.FromHours(18))
                    {
                        messageStr = messageStr + "Добрий день";
                    }
                    else
                    {
                        messageStr = messageStr + "Добрий вечір";
                    }

                    messageStr = $"{messageStr}, {client.Name},";
                    HttpClient httpClient = new HttpClient();
                    switch (record.Status)
                    {
                        case Status.Wait:
                            messageStr = $"{messageStr} ваше авто {record.Brand} записано на {record.DateStart.ToString("dd/MM/yyyy HH:mm")}";
                            break;
                        case Status.End:
                            messageStr = $"{messageStr} ваше авто {record.Brand} готове до видачі";
                            break;
                        case Status.Work:
                            messageStr = $"{messageStr} ваше авто {record.Brand} взято в роботу,очікуйте SMS про завершення роботи";
                            break;
                        case Status.Abolition:
                            messageStr = $"{messageStr} за вашим авто {record.Brand} відмова";
                            break;
                    }

                    string phoneNumber = client.Phone;
                    if (phoneNumber != "")
                    {
                        if (!phoneNumber.StartsWith("38"))
                        {
                            phoneNumber = "38" + phoneNumber;
                        }
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml("<request><auth><login>380977259326</login><password>Flyboxx125</password></auth>" +
                            $"<message><from>SpaceDetail</from><text>{messageStr}</text><recipient>{phoneNumber}</recipient></message></request>");
                        HttpContent httpContent = new StringContent(xmlDocument.OuterXml, Encoding.UTF8, "application/xml");


                        HttpResponseMessage response = await httpClient.PostAsync("https://api.letsads.com", httpContent);
                        string responseStr = await response.Content.ReadAsStringAsync();
                    }
                }
                if (thisRecord != null)
                {
                    if (client != null)
                    {
                        thisRecord.Client = client;
                    }
                    thisRecord.Sum = record.Sum;
                    thisRecord.Discount = record.Discount;
                    thisRecord.Status = record.Status;
                    thisRecord.DateStart = record.DateStart;
                    thisRecord.DateEnd = record.DateEnd;
                    thisRecord.BodySize = record.BodySize;
                    thisRecord.BodyType = record.BodyType;
                    thisRecord.Brand = record.Brand;
                    thisRecord.NumberOfCar = record.NumberOfCar;
                    thisRecord.GasCount = record.GasCount;
                    await db.SaveChangesAsync();


                    //Edit Delete and Add works
                    //Edit Works

                    List<Work> worksToAdd = record.Works.Except(thisRecord.Works).ToList();
                    List<Work> worksToDelete = thisRecord.Works.Except(record.Works).ToList();
                    if (worksToAdd.Count != 0)
                    {
                        foreach (Work work in worksToAdd)
                        {
                            List<Employer> employers = new List<Employer>();
                            foreach (Employer i in work.Employers)
                            {
                                Employer? employer = db.employers.FirstOrDefault(u => u.Id == i.Id);
                                if (employer != null)
                                {
                                    employers.Add(employer);
                                }
                            }
                            if (employers == null || employers.Count == 0)
                            {
                                return BadRequest("Введіть працівника/ів");
                            }
                            Service? service = db.services.FirstOrDefault(u => u.Id == work.Service.Id);
                            if (service == null)
                            {
                                return BadRequest("Послугу не було знайдено");
                            }


                            Work thisWork = new Work()
                            {
                                Employers = employers,
                                Service = service,
                                Record = thisRecord,
                                Price = work.Price,
                                TruePrice = work.Price,
                            };
                            db.works.Add(thisWork);
                        }
                        await db.SaveChangesAsync();
                    }
                    if (worksToDelete.Count != 0)
                    {
                        foreach (Work work in worksToDelete)
                        {
                            db.works.Remove(work);
                            await db.SaveChangesAsync();
                        }
                    }

                    foreach (Work workInEditRecord in record.Works)
                    {
                        if (workInEditRecord.Id >= 0)
                        {

                            Work? workInDataBase = thisRecord.Works.FirstOrDefault(u => u.Id == workInEditRecord.Id);

                            if (workInDataBase != null && (workInDataBase.DescriptionCost != workInEditRecord.DescriptionCost
                                || workInDataBase.PriceCost != workInEditRecord.PriceCost || workInDataBase.Price != workInEditRecord.Price
                                || workInDataBase.TruePrice != workInEditRecord.TruePrice || workInDataBase.Service.Id != workInEditRecord.Service.Id ||
                                !workInDataBase.Employers.SequenceEqual(workInEditRecord.Employers)))
                            {
                                List<Employer> employers = new List<Employer>();
                                foreach (Employer i in workInEditRecord.Employers)
                                {
                                    Employer? employer = db.employers.FirstOrDefault(u => u.Id == i.Id);
                                    if (employer != null)
                                    {
                                        employers.Add(employer);
                                    }
                                }
                                Service? service = db.services.FirstOrDefault(u => u.Id == workInEditRecord.Service.Id);
                                if (service != null && employers.Count > 0)
                                {
                                    db.works.Remove(workInDataBase);
                                    await db.SaveChangesAsync();
                                    Work EditedWork = new Work()
                                    {
                                        Service = service,
                                        Employers = employers,
                                        Price = workInEditRecord.Price,
                                        TruePrice = workInEditRecord.TruePrice,
                                        DescriptionCost = workInEditRecord.DescriptionCost,
                                        PriceCost = workInEditRecord.PriceCost,
                                        Record = thisRecord,
                                    };
                                    await db.works.AddAsync(EditedWork);
                                    await db.SaveChangesAsync();

                                }

                            }
                        }
                    }
                    //thisRecord.Client = record.Client;
                    //await db.SaveChangesAsync();
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("Запис не знайдено");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        [ActionName("DeleteRecord")]
        public async Task<ActionResult> DeleteRecord(int Id)
        {
            try
            {
                Record? record = await db.records.Where(u => u.Id == Id).FirstOrDefaultAsync();
                if (record != null)
                {
                    db.records.Remove(record);
                    await db.SaveChangesAsync();
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("Запис не знейдено");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [ActionName("GetRecordsQuick")]
        public ActionResult<List<Record>> GetRecordsQuick()
        {
            try
            {
                IEnumerable<Record> records = db.records
                .Include(u => u.Client)
                .OrderByDescending(p => p.Id)
                .Take(20);
                return Ok(records);
            }
            catch
            {
                return BadRequest(new List<Record>());
            }
        }
        [HttpGet]
        [ActionName("GetRecords")]
        public ActionResult<List<Record>> GetRecords()
        {
            try
            {
                List<Record> records = db.records.Include(u => u.Client)
                    .Include(m => m.Works).ThenInclude(i => i.Service).AsNoTracking()
                    .Include(j => j.Works).ThenInclude(l => l.Employers).AsNoTracking()
                    .OrderByDescending(p => p.Id).ToList();
                return Ok(records);
            }
            catch
            {
                return BadRequest(new List<Record>());
            }
        }
        [HttpGet("{Id}")]
        [ActionName("GetRecord")]
        public async Task<ActionResult<Record>> GetRecord(int Id)
        {
            try
            {
                Record? record = await db.records.Include(u => u.Works).ThenInclude(a => a.Employers).AsNoTracking()
                    .Include(t => t.Works).ThenInclude(j => j.Service).AsNoTracking()
                    .Include(a => a.Client).AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == Id);
                if (record != null)
                {
                    return Ok(record);
                }
                else
                {
                    return BadRequest(new Record());
                }
            }
            catch
            {
                return BadRequest(new Record());
            }
        }

        [HttpGet("{Id}")]
        [ActionName("GetRecordPhotos")]
        public async Task<ActionResult<List<Photo>>> GetRecordPhotos(int Id)
        {
            try
            {
                Record? record = await db.records.Include(u => u.Photos).FirstOrDefaultAsync(o => o.Id == Id);
                if (record != null)
                {
                    List<Photo> photos = record.Photos.ToList();
                    return Ok(photos);
                }
                return BadRequest(new List<Photo>());
            }
            catch
            {
                return BadRequest(new List<Photo>());
            }
        }
        [HttpPost("{Id}")]
        [ActionName("AddPhotos")]
        public async Task<ActionResult> AddPhotos(int Id, List<Photo> photos)
        {
            try
            {
                Record? record = await db.records.Include(u => u.Photos).FirstOrDefaultAsync(o => o.Id == Id);
                if (record != null)
                {
                    if (photos == null || photos.Count == 0)
                    {
                        BadRequest("Не додано фото");
                    }
                    else
                    {
                        foreach (var photo in photos)
                        {
                            db.photos.Add(photo);
                        }
                        await db.SaveChangesAsync();
                        foreach (var photo in photos)
                        {
                            record.Photos.Add(photo);
                        }
                        await db.SaveChangesAsync();
                        Ok("ok");
                    }
                }
                return BadRequest("Запис не знайдено");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{Id}")]
        [ActionName("AddPhoto")]
        public async Task<ActionResult> AddPhoto(int Id, Photo photo)
        {
            try
            {
                Record? record = await db.records.Include(u => u.Photos).FirstOrDefaultAsync(o => o.Id == Id);
                if (record != null)
                {
                    if (photo == null)
                    {
                        BadRequest("Не додано фото");
                    }
                    else
                    {
                        Photo thisPhoto = new Photo()
                        {
                            Bytes = photo.Bytes,
                            FileExtention = photo.FileExtention,
                            Size = photo.Size,
                            Type = photo.Type,
                            Record = record
                        };
                        db.photos.Add(thisPhoto);
                        await db.SaveChangesAsync();
                        return Ok("ok");
                    }
                }
                return BadRequest("Запис не знайдено");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        [ActionName("DeletePhoto")]
        public async Task<ActionResult> DeletePhoto(int Id)
        {
            try
            {
                Photo? photo = await db.photos.Where(u => u.Id == Id).FirstOrDefaultAsync();
                if (photo != null) {
                    db.photos.Remove(photo);
                    await db.SaveChangesAsync();
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("Фото не знайдено");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        [ActionName("GetPhoto")]
        public async Task<ActionResult<Photo>> GetPhoto(int Id, int NumPhoto)
        {
            try
            {
                var photo = await db.records
                .Where(record => record.Id == Id)
                .SelectMany(record => record.Photos)
                .Skip(NumPhoto)
                .FirstOrDefaultAsync();
                if (photo != null)
                {
                    return Ok(photo);
                }

                return Ok(new Photo());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("GetCosts")]
        public async Task<ActionResult<List<Cost>>> GetCosts()
        {
            try
            {
                var costs = await db.costs.ToListAsync();
                return Ok(costs);
            }
            catch
            {
                return new List<Cost>();
            }
        }
        [HttpPost]
        [ActionName("AddCost")]
        public async Task<ActionResult> AddCost(Cost cost)
        {
            try
            {
                db.costs.Add(new Cost() {
                    Name = cost.Name,
                    Description = cost.Description,
                    Date = cost.Date,
                    Price = cost.Price,
                });
                await db.SaveChangesAsync();
                return Ok("ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{Id}")]
        [ActionName("DeleteCost")]
        public async Task<ActionResult> DeleteCost(int Id)
        {
            try
            {
                var cost = await db.costs.FirstOrDefaultAsync(u => u.Id == Id);
                if (cost != null)
                {
                    db.costs.Remove(cost);
                    await db.SaveChangesAsync();
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("Не знайдено витрату");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("EditCost")]
        public async Task<ActionResult> EditCost(Cost cost)
        {
            try
            {
                Cost? thisCost = await db.costs.Where(u => u.Id == cost.Id).FirstOrDefaultAsync();
                if (thisCost != null)
                {
                    thisCost.Name = cost.Name;
                    thisCost.Description = cost.Description;
                    thisCost.Date = cost.Date;
                    thisCost.Price = cost.Price;
                    await db.SaveChangesAsync();
                    return Ok("ok");
                }
                else
                {
                    return BadRequest("Не знайдено витрати");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("AddSalary")]
        public async Task<ActionResult> AddSalary(Salary salary)
        {
            try
            {
                Employer? employer = await db.employers.FirstOrDefaultAsync(u => u.Id == salary.EmployerId);
                if (employer != null)
                {
                    await db.salaries.AddAsync(new Salary {
                        Date = salary.Date,
                        Description = salary.Description,
                        Employer = employer,
                        Value = salary.Value
                    });
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("не знайдено працівника");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [ActionName("EditSalary")]
        public async Task<ActionResult> EditSalary(Salary salary)
        {
            try
            {
                Salary? salaryFromDb = await db.salaries.FirstOrDefaultAsync(u => u.Id == salary.Id);
                if (salary != null)
                {
                    salaryFromDb.Value = salary.Value;
                    salaryFromDb.Description = salary.Description;
                    salaryFromDb.Date = salary.Date;
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("не знайдено зарплату");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{Id}")]
        [ActionName("DeleteSalary")]
        public async Task<ActionResult> DeleteSalary(int Id)
        {
            try
            {
                Salary? salaryFromDb = await db.salaries.FirstOrDefaultAsync(u => u.Id == Id);
                if (salaryFromDb != null)
                {
                    db.salaries.Remove(salaryFromDb);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("не знайдено зарплату");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{Id}")]
        [ActionName("GetSalarysEmployer")]
        public async Task<ActionResult<List<Salary>>> GetSalarysEmployer(int Id)
        {
            try
            {
                var salarys = await db.salaries.Where(u => u.EmployerId == Id).ToListAsync();
                return Ok(salarys);
            }
            catch
            {
                return new List<Salary>();
            }
        }
        [HttpGet]
        [ActionName("GetSalarys")]
        public async Task<ActionResult<List<Salary>>> GetSalarys()
        {
            try
            {
                var salarys = await db.salaries.Include(u => u.Employer).AsNoTracking().ToListAsync();
                return Ok(salarys);
            }
            catch
            {
                return new List<Salary>();
            }
        }
    }
}
