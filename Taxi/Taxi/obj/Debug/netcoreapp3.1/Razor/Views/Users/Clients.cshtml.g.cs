#pragma checksum "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "743b7d4805d955625910d9ea3936374c1502c83c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Clients), @"mvc.1.0.view", @"/Views/Users/Clients.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/_ViewImports.cshtml"
using Taxi;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/_ViewImports.cshtml"
using Taxi_Database.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"743b7d4805d955625910d9ea3936374c1502c83c", @"/Views/Users/Clients.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fd098e98604c6a2d363ef032ab538a4d2daedd0", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_Clients : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Taxi_Database.Models.Client>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link flex-nowrap nowrap text-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Users", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Clients", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Location", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "History", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("modal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-target", new global::Microsoft.AspNetCore.Html.HtmlString("#staticBackdrop"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col card-link text-danger ml-0 pb-0 mb-0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\n<html lang=\"en\">\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "743b7d4805d955625910d9ea3936374c1502c83c6349", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <title>Локации</title>
    <link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css""
          integrity=""sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh"" crossorigin=""anonymous"">
    <script src=""https://code.jquery.com/jquery-3.4.1.slim.min.js""
            integrity=""sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n""
            crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js""
            integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo""
            crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""
            integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6""
            crossorigin=""anonymous""></script>
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery");
                WriteLiteral(@".min.js""></script>
    <script src=""https://rawgit.com/jackmoore/autosize/master/dist/autosize.min.js""></script>

    <style>


        .nowrap {
            white-space: nowrap
        }

        .find {
            display: block;
            transition: all 300ms ease-out;
        }

        .transition {
            transition: all 300ms ease-out;
        }

        .subscribe {
            display: block;
            transition: all 300ms ease-out;
        }

        .high {
            background: #5a28b3;
            color: white;
        }

        .text-black {
            color: black;
        }

        .card {
            transition: all 600ms ease-out;
        }

        .card-hover {
            border-color: #000000;
            -moz-box-shadow: 0 0 12px #000000;
            -webkit-box-shadow: 0 0 12px #000000;
            box-shadow: 0 0 12px #000000;
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "743b7d4805d955625910d9ea3936374c1502c83c9291", async() => {
                WriteLiteral(@"
    <nav id=""nav"" class=""navbar navbar-expand-lg  navbar-dark bg-dark fixed-top "">
        <div class=""row  row-cols-2  ml-md-0 mr-md-0 mb-1 mt-1 align-content-stretch d-flex  flex-fill "">
            <div class=""col-auto  pl-md-0 pr-sm-2  align-content-center pb-0 transition"">
                <button type=""button""
                        class=""flex-fill btn btn-primary  pr-sm-4 pl-sm-4 pb-auto mb-0 font-weight-bold text-center find"">
                    Добавить локацию
                </button>
            </div>
            <div class=""col-auto mr-2 pl-sm-2 pr-sm-0 align-content-stretch transition"">
                <button type=""button""
                        class=""flex-fill btn btn-outline-light mr-sm-0 pl-sm-4 pr-sm-4 pb-auto mb-sm-0  font-weight-light  subscribe"">
                    Подписаться
                </button>
            </div>
            <div class=""mr-sm-3 ml-sm-3""></div>
        </div>
        <button class=""navbar-toggler ml-auto"" id=""nav-button"" type=""button"" data-toggle=""collapse""");
                WriteLiteral(@"
                data-target=""#navbarSupportedContent""
                aria-controls=""navbarSupportedContent"" aria-expanded=""false"" aria-label=""Toggle navigation"">
            <span class=""navbar-toggler-icon""></span>
        </button>
        <div class=""collapse navbar-collapse  justify-content-end"" id=""navbarSupportedContent"">
            <div class=""ml-auto  align-content-stretch  "">
                <div class=""input-group mb-1 mt-1 "">
                    <input type=""text"" id=""search"" class=""form-control bg-light border-light transition  flex-nowrap""
                           placeholder=""Поиск...""
                           aria-describedby=""button-addon2"">
                    <div class=""input-group-append"">
                        <span class=""input-group-text bg-light border-light"" id=""basic-addon1"">
                            <svg class=""bi bi-search"" width=""1em"" height=""1em"" viewBox=""0 0 16 16"" fill=""currentColor""
                                 xmlns=""http://www.w3.org/2000/svg"">
              ");
                WriteLiteral(@"                  <path fill-rule=""evenodd""
                                      d=""M10.442 10.442a1 1 0 011.415 0l3.85 3.85a1 1 0 01-1.414 1.415l-3.85-3.85a1 1 0 010-1.415z""
                                      clip-rule=""evenodd"" />
                                <path fill-rule=""evenodd"" d=""M6.5 12a5.5 5.5 0 100-11 5.5 5.5 0 000 11zM13 6.5a6.5 6.5 0 11-13 0 6.5 6.5 0 0113 0z""
                                      clip-rule=""evenodd"" />
                            </svg>
                        </span>
                    </div>
                </div>
            </div>
            <ul class=""navbar-nav ml-sm-2"">
                <li class=""nav-item "">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "743b7d4805d955625910d9ea3936374c1502c83c12437", async() => {
                    WriteLiteral("Пользователи");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                </li>\n                <li class=\"nav-item \">\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "743b7d4805d955625910d9ea3936374c1502c83c14028", async() => {
                    WriteLiteral("История локаций");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </li>
            </ul>
        </div>

    </nav>
    <div class=""container-sm p-4 "" style=""max-width: 40em; margin-top: 50px"">
        <div id=""titlesearch"">
            <div class=""radio-group flex-row d-flex flex-row mt-3 mb-5 justify-content-between align-content-center"">
                <div class=""align-self-center"">
                    <h1 class=""title my-auto"">Найдено<span class=""badge badge-primary transition  ml-sm-3""></span></h1>
                </div>
            </div>
        </div>
        <div id=""titleactual"">
            <h1 class=""title mt-3 mb-5 "">Пользователи<span class=""badge badge-primary ml-sm-3 "">");
#nullable restore
#line 125 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                                                                                           Write(Model.Count());

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></h1>\n        </div>\n");
#nullable restore
#line 127 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"col d-inline-flex ml-0 pl-0  pb-0 mb-0\">\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "743b7d4805d955625910d9ea3936374c1502c83c16867", async() => {
                    WriteLiteral("удалить");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 131 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                                                                                          WriteLiteral(item.StringId);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n            </div>\n            <div class=\"card mt-4 mb-4\">\n                <div class=\"card-body\">\n                    <h5 class=\"card-title\">");
#nullable restore
#line 135 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                                      Write(item.ClientName);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h5>
                    <table class=""table"">
                        <thead>
                            <tr>
                                <th scope=""col"">Map</th>
                                <th scope=""col"">Code</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Имя</td>
                                <td>");
#nullable restore
#line 146 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.FirstName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Фамилия</td>\n                                <td>");
#nullable restore
#line 150 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.SecondName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Почта</td>\n                                <td>");
#nullable restore
#line 154 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.Email);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Пароль</td>\n                                <td>");
#nullable restore
#line 158 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.Password);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Число поездок</td>\n                                <td>");
#nullable restore
#line 162 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.CountOfTrips);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Рейтинг</td>\n                                <td>");
#nullable restore
#line 166 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.Rating);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>О себе</td>\n                                <td>");
#nullable restore
#line 170 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.AboutSelf);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Число оценок</td>\n                                <td>");
#nullable restore
#line 174 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.CountOfRates);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Приоритет</td>\n                                <td>");
#nullable restore
#line 178 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.Priority);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                            <tr>\n                                <td>Время регистрации</td>\n                                <td>");
#nullable restore
#line 182 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
                               Write(item.RegisterTime);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n                            </tr>\n                        </tbody>\n                    </table>\n                </div>\n            </div>\n");
#nullable restore
#line 188 "/Users/ildardavletarov/Taxi_WebApplicationTimur/Taxi/Taxi/Views/Users/Clients.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    </div>
    <script>
$('.card ').hover(
            function () {
                $(this).addClass('border-dark card-hover')
            },
            function () {
                $(this).removeClass('border-dark card-hover')
            }
        );


        $("".subscribe"").hover(
            function () {
                $("".find"").removeClass('pr-sm-4 pl-sm-4');
                $(this).removeClass('pr-sm-4 pl-sm-4');
                $("".find"").addClass('pr-sm-2 pl-sm-2');
                $(this).addClass('pr-sm-5 pl-sm-5')
            },
            function () {
                $("".find"").removeClass('pr-sm-2 pl-sm-2');
                $(this).removeClass('pr-sm-5 pl-sm-5');
                $("".find"").addClass('pr-sm-4 pl-sm-4');
                $(this).addClass('pr-sm-4 pl-sm-4');
            }
        );
        $("".find"").hover(
            function () {
                $("".subscribe"").removeClass('pr-sm-4 pl-sm-4');
                $(this).removeClass('pr-sm-4 pl-sm-4');
                $("".sub");
                WriteLiteral(@"scribe"").addClass('pr-sm-2 pl-sm-2');
                $(this).addClass('pr-sm-5 pl-sm-5')
            },
            function () {
                $("".subscribe"").removeClass('pr-sm-2 pl-sm-2');
                $(this).removeClass('pr-sm-5 pl-sm-5');
                $("".subscribe"").addClass('pr-sm-4 pl-sm-4');
                $(this).addClass('pr-sm-4 pl-sm-4');
            }
        );
        $(""#nav-button"").click(function () {
            $("".find"").parent().toggleClass(""d-flex flex-fill"");
            $("".subscribe"").parent().toggleClass(""d-flex flex-fill"");
            $("".find"").parent().parent().toggleClass(""mr-sm-0"");
        });
        $(document).ready(function () {
            $(""#titlesearch"").hide();
            $(""#search"").keyup(search);
        });

        function search(withFilter) {
            let value = $(""#search"").val().toLowerCase();
            let words = value.split("" "");
            let count = 0;
            let searching = value.length !== 0;

            $("".card"").filter(fu");
                WriteLiteral(@"nction () {
                let totalFilter = true;
                let thisText = $(this).text().toLowerCase();
                for (let i = 0; i < words.length; i++) {
                    totalFilter = totalFilter && (thisText.indexOf(words[i]) > -1);
                }
                if (totalFilter) {
                    count++;
                }
                $(this).toggle(totalFilter);
            });
            if (searching) {//searching
                hideTitles(count, 400);
            } else {//stop searching
                showTitles(400);
            }
        }

        function hideTitles(count, delay) {
            $(""#titlesearch h1 span"").text(count);
            $(""#titleactual"").hide();
            $(""#titlesearch"").show();
            $(""#titlehistory"").hide();
        }

        function showTitles(delay) {
            $(""#titlesearch"").hide();
            $(""#titleactual"").show();
            $(""#titlehistory"").show();
        }

        $(""#search"").hover(
            function (");
                WriteLiteral(@") {
                $(this).addClass('pr-sm-5');
            },
            function () {
                $(this).removeClass('pr-sm-5');
            }
        );
        $(""#search"").focus(
            function () {
            },
            function () {
            }
        );</script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Taxi_Database.Models.Client>> Html { get; private set; }
    }
}
#pragma warning restore 1591
