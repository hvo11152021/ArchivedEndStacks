#pragma checksum "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f741a30647831786a03728af65b4fc73e159e404"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserRoles_Index), @"mvc.1.0.view", @"/Views/UserRoles/Index.cshtml")]
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
#line 1 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\_ViewImports.cshtml"
using CanadaGames;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\_ViewImports.cshtml"
using CanadaGames.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f741a30647831786a03728af65b4fc73e159e404", @"/Views/UserRoles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca7df126862f209c84e5945aadfc51472a2f68b", @"/Views/_ViewImports.cshtml")]
    public class Views_UserRoles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CanadaGames.ViewModels.UserVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
  
    ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>User Role Assignments</h2>\r\n\r\n<table class=\"table\">\r\n    <tr>\r\n        <th>\r\n            ");
#nullable restore
#line 12 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 15 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.UserRoles));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th></th>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 20 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 24 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
#nullable restore
#line 27 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
                  
                    foreach (var r in item.UserRoles)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            WriteLiteral("  ");
#nullable restore
#line 30 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
                       Write(r);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />\r\n");
#nullable restore
#line 31 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 35 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 38 "C:\Hy_term3_mvc_projects\CanadaGames_Part4\CanadaGames\Views\UserRoles\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CanadaGames.ViewModels.UserVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
