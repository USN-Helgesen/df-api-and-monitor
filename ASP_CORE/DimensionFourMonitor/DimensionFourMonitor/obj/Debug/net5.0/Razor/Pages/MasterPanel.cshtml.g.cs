#pragma checksum "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ba974182492e7b3dd031308b49f18790b1fd33d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(DimensionFourMonitor.Pages.Pages_MasterPanel), @"mvc.1.0.razor-page", @"/Pages/MasterPanel.cshtml")]
namespace DimensionFourMonitor.Pages
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
#line 1 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\_ViewImports.cshtml"
using DimensionFourMonitor;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba974182492e7b3dd031308b49f18790b1fd33d3", @"/Pages/MasterPanel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b6ab9691285feedf50ea1b1d85c07be4f90711d", @"/Pages/_ViewImports.cshtml")]
    public class Pages_MasterPanel : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
  
    ViewData["Title"] = "Master Display Panel";

#line default
#line hidden
#nullable disable
            WriteLiteral("<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba974182492e7b3dd031308b49f18790b1fd33d33883", async() => {
                WriteLiteral(@"
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js""></script>
    <script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
    <script type=""text/javascript"">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn(['datetime', 'Time']);
            data.addColumn('number', 'Value');

            var options = {
                title: 'Signals',
                curveType: 'function',
                pointsVisible: true,
                lineWidth: 3,
                legend: 'none',
                hAxis: { title: 'Time' },
                vAxis: { title: 'Data' },
                width: '100%',
                height: '100%',
                chartArea: { width: '85%', height: '75%' }
            };

            var chart = new google.visuali");
                WriteLiteral(@"zation.LineChart(document.getElementById('curve_chart'));

            chart.draw(data, options);
        }
        function updateChart(id) {
            console.log(""got this far"");
            if (id != null) {
                console.log(""and this"");
                var type = ""testtemp"";
                var paginate = 15;
                $.ajax({
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: {
                        id: id,
                        type: type,
                        paginate: paginate
                    },
                    url: '/MasterPanel?handler=UpdateSignals',
                    success: function (result) {
                        var data = new google.visualization.DataTable();
                        var items = result;
                        data.addColumn(['datetime', 'Time']);
                        data.addColumn('number', 'Value');

       ");
                WriteLiteral(@"                 for (var i = 0; i < items.length; i++) {
                            var item = items[i];
                            data.addRow([item.timestamp, Number(item.value)]);
                        }

                        var options = {
                            title: 'Signals',
                            curveType: 'function',
                            pointsVisible: true,
                            lineWidth: 3,
                            legend: 'none',
                            hAxis: { title: 'Time' },
                            vAxis: { title: 'Data' },
                            width: '100%',
                            height: '100%',
                            chartArea: { width: '85%', height: '75%' }
                        };

                        var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
                        chart.draw(data, options);
                    },
                    error: function (x");
                WriteLiteral("hr, status, error) {\r\n                        var err = eval(\"(\" + xhr.responseText + \")\");\r\n                        alert(err.Message);\r\n                    }\r\n                });\r\n            }\r\n        }\r\n    </script>\r\n");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba974182492e7b3dd031308b49f18790b1fd33d38282", async() => {
                WriteLiteral("\r\n    <div class=\"row\">\r\n        <div class=\"column\">\r\n            <h1>Spaces and Points</h1>\r\n\r\n            <p>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba974182492e7b3dd031308b49f18790b1fd33d38682", async() => {
                    WriteLiteral("Create New");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
            </p>
            <table class=""table"">
                <thead>
                    <tr>
                        <th>
                            Space Name
                        </th>
                        <th>
                            Space ID
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 109 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                     foreach (var item in Model.spaces)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <tr class=\"space_row\">\r\n                            <td>\r\n                                ");
#nullable restore
#line 113 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                           Write(Html.DisplayFor(modelItem => item.name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 116 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                           Write(Html.DisplayFor(modelItem => item.id));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                            </td>
                        </tr>
                        <tr class=""point_header_row"">
                            <th style=""padding-left:50px"">
                                Point Name
                            </th>
                            <th style=""padding-left:50px"">
                                Point ID
                            </th>
                        </tr>
");
#nullable restore
#line 127 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                         foreach (var point in item.points)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <tr class=\"point_row\">\r\n                                <td style=\"padding-left:50px\">\r\n                                    ");
#nullable restore
#line 131 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                               Write(Html.DisplayFor(modelItem => point.name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </td>\r\n                                <td class=\"point_id\", style=\"padding-left:50px\">\r\n                                    ");
#nullable restore
#line 134 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                               Write(Html.DisplayFor(modelItem => point.id));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 137 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 137 "C:\Users\Håkon\Documents\Python\Master\df-api-and-monitor\ASP_CORE\DimensionFourMonitor\DimensionFourMonitor\Pages\MasterPanel.cshtml"
                         
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n        <div class=\"column\">\r\n            <div id=\"curve_chart\" style=\"width: 650px; height: 500px\"></div>\r\n        </div>\r\n    </div>\r\n");
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
            WriteLiteral(@"
</html>
<script>
    $(document).ready(function () {
        $('.space_row').nextUntil('tr.space_row').slideToggle(10);
        $('.space_row').click(function () {
            $(this).nextUntil('tr.space_row').slideToggle(400);
        });
        $('.point_id').click(function (event) {
            var id = $(event.target).text().trim();
            updateChart(id);
        });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DimensionFourMonitor.Pages.MasterPanelModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DimensionFourMonitor.Pages.MasterPanelModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DimensionFourMonitor.Pages.MasterPanelModel>)PageContext?.ViewData;
        public DimensionFourMonitor.Pages.MasterPanelModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591