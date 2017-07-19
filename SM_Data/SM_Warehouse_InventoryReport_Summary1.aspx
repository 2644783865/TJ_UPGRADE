<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_InventoryReport_Summary1.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventoryReport_Summary1" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dth">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>汇总1</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link href="javascripts/superTables.css" rel="stylesheet" type="text/css" />

    <script src="javascripts/superTables.js" type="text/javascript"></script>

    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <style type="text/css">
        body
        {
            background-color: #ffffff;
            font-family: Tahoma, Arial, sans serif;
        }
        .fakeContainer
        {
            /* The parent container */
            margin-top: 20px;
            margin-right: auto;
            margin-left: auto;
            padding: 0px;
            border: none;
            width: 1300px; /* Required to set */
            height: 500px; /* Required to set */
            overflow: hidden; /* Required to set */
        }
        .odd
        {
            background: #ffc;
        }
        .even
        {
            background: #cef;
        }
    </style>

    <script language="javascript" type="text/javascript">

     function DownloadFile() {
        var val = "SM_WarehouseStorage_Export.aspx?file=Files";
        
//        alert(val);
        var dn = new AjaxDownload(val);
        dn.EnableTrace(true);
        //fires before download, 
        dn.add_onBeginDownload(BeginDownload);
        dn.add_onEndDownload(EndDownload);
        dn.add_onError(DownloadError);
        dn.Download();
        return true;
    }
     function BeginDownload() {
        $.blockUI(); 
    }
    
    function EndDownload() {
        $.unblockUI();
    }
    
    
    function DownloadError() {
        var errMsg = AjaxDownload.ErrorMessage;
    }
    
function MouseDown(obj)
{
	obj.mouseDownX=event.clientX;
	obj.pareneTdW=obj.parentElement.offsetWidth;
	obj.pareneTbW=myTable.offsetWidth;
	obj.setCapture();
}

function MouseMove(obj)
{
	if(!obj.mouseDownX) return false;
	var newWidth=obj.pareneTdW*1+event.clientX*1-obj.mouseDownX;
	if(newWidth>0)
	{
		obj.parentElement.style.width = newWidth;
		myTable.style.width=obj.pareneTbW*1+event.clientX*1-obj.mouseDownX;
	}
}

function MouseUp(obj)
{
	obj.releaseCapture();
	obj.mouseDownX=0;
}

//一些常用的，往往又会搞混的常用代码
//var s = "网页可见区域宽：" + document.body.clientWidth;     
//s  +=  "<br>网页可见区域高：" +  document.body.clientHeight;     
//s  +=  "<br>网页可见区域宽：" +  document.body.offsetWidth + " (包括边线的宽)";  
//s  +=  "<br>网页可见区域高：" +  document.body.offsetHeight + " (包括边线的高)";
//s  +=  "<br>网页正文全文宽：" +  document.body.scrollWidth;     
//s  +=  "<br>网页正文全文高：" +  document.body.scrollHeight;  
//s  +=  "<br>网页正文部分上：" +  window.screenTop;     
//s  +=  "<br>网页正文部分左：" +  window.screenLeft;     
//s  +=  "<br>屏幕分辨率的高：" +  window.screen.height;     
//s  +=  "<br>屏幕分辨率的宽：" +  window.screen.width;     
//s  +="<br>屏幕可用工作区高度：" +  window.screen.availHeight;     
//s  +="<br>屏幕可用工作区宽度：" +  window.screen.availWidth;    
// 
//document.write(s);     

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                        <asp:Label ID="LabelWarehouse" runat="server" Visible="false" ></asp:Label>
                            <asp:Label ID="LabelTime" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="ExportSummary1" runat="server" Text="导出" OnClick="ExportSummary1_Click"
                                OnClientClick="DownloadFile();" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="sumnuminaccount" runat="server" />
    <asp:HiddenField ID="sumfzninaccount" runat="server" />
    <asp:HiddenField ID="sumamountinaccount" runat="server" />
    <asp:HiddenField ID="sumnotinnum" runat="server" />
    <asp:HiddenField ID="sumnotinfznum" runat="server" />
    <asp:HiddenField ID="sumnotinamount" runat="server" />
    <asp:HiddenField ID="sumnotoutnum" runat="server" />
    <asp:HiddenField ID="sumnotoutfznum" runat="server" />
    <asp:HiddenField ID="sumnotoutamount" runat="server" />
    <asp:HiddenField ID="sumduenum" runat="server" />
    <asp:HiddenField ID="sumduefznum" runat="server" />
    <asp:HiddenField ID="sumdueamount" runat="server" />
    <asp:HiddenField ID="sumrealnum" runat="server" />
    <asp:HiddenField ID="sumrealfznum" runat="server" />
    <asp:HiddenField ID="sumrealamount" runat="server" />
    <asp:HiddenField ID="sumdiffnum" runat="server" />
    <asp:HiddenField ID="sumdifffznum" runat="server" />
    <asp:HiddenField ID="sumdiffamount" runat="server" />
    <div class="fakeContainer">
        <table id="demoTable">
            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound"
                EnableViewState="False">
                <HeaderTemplate>
                    <tr style="background-color: #CFDCEE">
                        <td nowrap="nowrap">
                            <strong>序号</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>物料编码</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>物料名称</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>材质</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>规格型号</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>国标</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>长</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>宽</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>单位</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>单重</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>账面数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>账面张(支)数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>账面金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未入库数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未入库张（支）数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未入库金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未出库数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未出库张（支）数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>尚未出库金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>应结存数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>应结存张（支）数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>应结存金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>实际数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>实际张（支）数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>实际金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>差异数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>差异张（支）数</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>差异金额</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td nowrap="nowrap">
                            <%#Container.ItemIndex+1%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "MaterialName")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "Attribute")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "GB")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "Length")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "Width")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "Unit")%>
                        </td>
                        <td nowrap="nowrap">
                            <%#DataBinder.Eval(Container.DataItem, "UnitWeight")%>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SNInAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumNotIn" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotIn")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNNotIn" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "SNNotIn")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountNotIn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotIn")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumNotOut" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotOut")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNNotOut" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "SNNotOut")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountNotOut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotOut")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDueToAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SNDueToAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountDueToAccount")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInventory")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SNInventory")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInventory")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelNumDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDiff")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelSNDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SNDiff")%>'></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="LabelAmountDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountDiff")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <strong>合计：</strong>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum4" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum5" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum6" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum7" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum8" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum9" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum10" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum11" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum12" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum13" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum14" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum15" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum16" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum17" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelSum18" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <strong>总计：</strong>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Sum1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum4" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum5" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum6" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum7" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum8" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum9" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum10" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum11" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum12" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum13" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum14" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum15" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum16" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum17" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Sum18" runat="server"></asp:Label>
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPanel2" runat="server" Visible="false">
            没有相关记录!</asp:Panel>
    </div>
    <uc1:UCPaging ID="UCPaging2" runat="server" Visible="false" />

    <script type="text/javascript">
//<![CDATA[
var width=$("#demoTable").width();

if(width<=1300)
{
$("div.fakeContainer").width($("#demoTable").width()+50);
}
else
{
$("div.fakeContainer").width(1300);
}

(function() {
	var mySt = new superTable("demoTable", {
		cssSkin : "sSky",
		fixedCols : 2,
		headerRows : 1,
		onStart : function () {
			
		},
		onFinish : function () {
		  
		   for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
           {
                 this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                 {
                                var clicked = false;
                                var dataRow = this.sDataTable.tBodies[0].rows[i];
                                var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                                return function () 
                                      {
                                            if (clicked) 
                                            {
//                                                if(i%2==0)
//                                                {
//                                                //偶数
//                                                    dataRow.style.backgroundColor = "#cef";
//                                                    fixedRow.style.backgroundColor = "#cef";
//                                                    clicked = false;
//                                                }
//                                                else
//                                                {
                                                //奇数
//                                                    dataRow.style.backgroundColor = "#ffc";
//                                                    fixedRow.style.backgroundColor = "#ffc";
//                                                    clicked = false;
//                                                }
                                                    dataRow.style.backgroundColor = "#fff";
                                                    fixedRow.style.backgroundColor = "#e4ecf7";
                                                    clicked = false;
                                                
                                            }
                                            else 
                                            {
                                                dataRow.style.backgroundColor = "#eeeeee";
                                                fixedRow.style.backgroundColor = "#adadad";
                                                clicked = true;
                                            }
                                        }
                                       
                 }.call(this, i);
           }
		}
	});
})();
//设置表头的高度

var height=$("div.sFData>div.sFDataInner>table tr").height();

$("div.sFHeader>table").height(height-4);
$("div.sFHeader>table tr").height(height-4);
$("div.sFHeader>table td").height(height-4);
$("div.sHeader>div.sHeaderInner>table").height(height-4);
$("div.sHeader>div.sHeaderInner>table tr").height(height-4);
$("div.sHeader>div.sHeaderInner>table td").height(height-4);

//颜色
//$("div.sFData>div.sFDataInner>table tr:odd").addClass('odd');
//$("div.sFData>div.sFDataInner>table tr:even").addClass('even');

//$("div.sData>table tr:odd").addClass('odd');
//$("div.sData>table tr:even").addClass('even');

//]]>
    </script>

    </form>
</body>
</html>
