using AeC.Domains;
using AeC.Repositories.Interfaces;
using AeC.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Collections.ObjectModel;

namespace AeC.Services
{
    public class PesquisaService : ServiceBase<Pesquisa>, IPesquisaService
    {
        private readonly IRepository<Pesquisa> _repository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private const string cENTER = "\r\n";

        public PesquisaService(IRepository<Pesquisa> repository, IConfiguration configuration, HttpClient httpClient, RestClient restClient) : base(repository)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
            _restClient = restClient;
            //_httpClient.BaseAddress = new Uri(_configuration.GetConnectionString("BaseUrl"));

            //_httpClient.BaseAddress = new Uri(_configuration.GetValue( "BaseUrl"));
            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("BaseUrl"));
        }

        public IEnumerable<Pesquisa> BuscaPorTermo(string termo)
        {
            IWebDriver webDriver = ObterWebDriver();

            ClicarNoBocaoPesquisar(webDriver);

            PesquisarTermoDigitado(termo, webDriver);

            ReadOnlyCollection<IWebElement> links;

            List<Pesquisa> pesquisas;

            ObterResultadoDaPesquisa(webDriver, out links, out pesquisas);

            GuardarDados(webDriver, links, pesquisas);

            return pesquisas;
        }

        private void GuardarDados(IWebDriver webDriver, ReadOnlyCollection<IWebElement> links, List<Pesquisa> pesquisas)
        {
            foreach (var link in links)
            {
                var div = link.FindElement(By.CssSelector("div.text.gray"));
                var span = div.FindElement(By.CssSelector("span.hat"));
                var autorEData = div.FindElement(By.CssSelector("span:nth-child(3) > small"));

                var titulo = link.GetAttribute("title");
                var area = span.Text;
                var autor = autorEData.Text;
                var descricao = "";
                var dataPublicacao = "";

                //Thread.Sleep(2000);
                
                link.Click();

                Thread.Sleep(500);
                var divContents = webDriver.FindElements(By.CssSelector("body > main > div.section-single-blog > div > div > div.col-lg-9"));
                if (divContents.Any())
                {
                    var divContent = divContents.First();

                    dataPublicacao = divContent.FindElement(By.CssSelector("div.info-in-single-blog > ul > li:nth-child(2) > a")).Text;

                    var main = divContent.FindElement(By.CssSelector("div.content-wp-in-single-blog"));

                    var elements = main.FindElements(By.CssSelector("h1, h2, h3, h4, h5, h6, p"));
                    foreach (var element in elements.Skip(1))
                        descricao += element.Text + cENTER;

                    Console.WriteLine($@"titulo = {titulo}\r\nautor = {autor}\r\narea = {area}\r\ndataPublicacao = {dataPublicacao}\r\ndescricao = {descricao}");
                }

                var pesquisaEncontrada = new Pesquisa
                {
                    Area = area,
                    Autor = autor,
                    DataDePublicacao = dataPublicacao,
                    Descricao = descricao,
                    Titulo = titulo
                };

                pesquisas.Add(pesquisaEncontrada);

                _repository.Save(pesquisaEncontrada);

                webDriver.Navigate().Back();

            }
        }

        private static void ObterResultadoDaPesquisa(IWebDriver webDriver, out ReadOnlyCollection<IWebElement> links, out List<Pesquisa> pesquisas)
        {
            Thread.Sleep(500);
            var divResultado = webDriver.FindElement(By.CssSelector("body > main > div.category > div > strong > div.container > div > div > div"));
            links = divResultado.FindElements(By.CssSelector("a"));
            pesquisas = new List<Pesquisa>();
        }

        private static void PesquisarTermoDigitado(string termo, IWebDriver webDriver)
        {
            Thread.Sleep(500);
            var btnK = webDriver.FindElement(By.CssSelector("#form > input[type=search]"));
            btnK.SendKeys(termo + cENTER);
        }

        private static void ClicarNoBocaoPesquisar(IWebDriver webDriver)
        {
            Thread.Sleep(500);
            var pesquisa = webDriver.FindElement(By.CssSelector("#header > div.body > div > div > div > div > ul.align-items-center.justify-content-end.d-none.d-xl-flex.mb-0.ps-0 > li:nth-child(2) > a"));
            pesquisa.Click();
        }

        private static IWebDriver ObterWebDriver()
        {
            var webDriver = GetChromeDriver();
            //webDriver.Navigate().GoToUrl(_httpClient.BaseAddress);
            webDriver.Navigate().GoToUrl("https://www.aec.com.br/");
            return webDriver;
        }

        public static IWebDriver GetChromeDriver()
        {
            var fileLocation = new FileInfo(Path.Combine(".", "WebDriver", "ChromeDriver.exe"));
            var driverService = ChromeDriverService.CreateDefaultService(fileLocation.Directory.FullName, fileLocation.Name);
            var driverOptions = new ChromeOptions { AcceptInsecureCertificates = true, PageLoadStrategy = PageLoadStrategy.Normal };
            return new ChromeDriver(driverService, driverOptions, TimeSpan.FromSeconds(30));
        }
    }
}
