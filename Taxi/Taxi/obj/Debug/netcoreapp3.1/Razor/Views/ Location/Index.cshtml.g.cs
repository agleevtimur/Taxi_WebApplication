#pragma checksum "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fda97422b64aef5491ed3d6717c93adaee0f35e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views__Location_Index), @"mvc.1.0.view", @"/Views/ Location/Index.cshtml")]
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
#line 1 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\_ViewImports.cshtml"
using Taxi;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\_ViewImports.cshtml"
using Taxi_Database.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fda97422b64aef5491ed3d6717c93adaee0f35e", @"/Views/ Location/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e40fcd57fb6224475aa68ca6977d70bd4ea86e3", @"/Views/_ViewImports.cshtml")]
    public class Views__Location_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Taxi_Database.Models.Location>>
    {
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9fda97422b64aef5491ed3d6717c93adaee0f35e3297", async() => {
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
    <script src=""https://ajax.googleapis.com/ajax/libs/jquer");
                WriteLiteral(@"y/2.1.1/jquery.min.js""></script>
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
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9fda97422b64aef5491ed3d6717c93adaee0f35e6314", async() => {
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
        <button class=""navbar-toggler ml-auto"" id=""nav-button"" type=""button"" data-");
                WriteLiteral(@"toggle=""collapse""
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
                                 xmlns=""http://www.w");
                WriteLiteral(@"3.org/2000/svg"">
                                <path fill-rule=""evenodd""
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
                    <a class=""nav-link flex-nowrap nowrap text-right"" href=""#"">О Проекте<span class=""sr-only""></span></a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link flex-nowrap nowrap text-right"" href=""#"">Мои заказы<span class=""sr-only""></span></a>
      ");
                WriteLiteral(@"          </li>

                <li class=""nav-item "">
                    <a class=""nav-link flex-nowrap nowrap text-right"" href=""#"">Мой профиль<span class=""sr-only""></span></a>
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
            <h1 class=""title mt-3 mb-5 "">Локации<span class=""badge badge-primary ml-sm-3 "">");
#nullable restore
#line 129 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
                                                                                      Write(Model.Count());

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></h1>\r\n        </div>\r\n");
#nullable restore
#line 131 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"card mt-4 mb-4\">\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">");
#nullable restore
#line 135 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
                                      Write(item.NameOfLocation);

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
                                <td>Yandex</td>
                                <td>");
#nullable restore
#line 146 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
                               Write(item.YandexCode);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <td>2Gis</td>\r\n                                <td>");
#nullable restore
#line 150 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
                               Write(item.TwoGisCode);

#line default
#line hidden
#nullable disable
                WriteLiteral(".</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <td>Google</td>\r\n                                <td>");
#nullable restore
#line 154 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
                               Write(item.GoogleCode);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 160 "C:\Users\fanto\Desktop\gitTaxiWeb\dmitrii\Taxi\Taxi\Views\ Location\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    </div>
    <script>$('.card ').hover(
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
                $(this).removeClass('pr-sm-4 pl-sm-");
                WriteLiteral(@"4');
                $("".subscribe"").addClass('pr-sm-2 pl-sm-2');
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
            let searching = ");
                WriteLiteral(@"value.length !== 0;

            $("".card"").filter(function () {
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
            $(""#t");
                WriteLiteral(@"itlehistory"").show();
        }

        $(""#search"").hover(
            function () {
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
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Taxi_Database.Models.Location>> Html { get; private set; }
    }
}
#pragma warning restore 1591
