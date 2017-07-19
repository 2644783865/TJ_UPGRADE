<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_InventoryReport_Summary2.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventoryReport_Summary2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>汇总2</title>
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
          
            margin-top: 20px; margin-right: auto; margin-left: auto;
            padding: 0px;
            border: none;
            width: 1000px; /* Required to set */
            height: 500px; /* Required to set */
            overflow: hidden; /* Required to set */
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
                            <asp:Button ID="ExportSummary2" runat="server" Text="导出" OnClick="ExportSummary1_Click"
                                OnClientClick="DownloadFile();" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="sumnuminaccount" runat="server" />
    <asp:HiddenField ID="sumamountinaccount" runat="server" />
    <asp:HiddenField ID="sumnotinnum" runat="server" />
    <asp:HiddenField ID="sumnotinamount" runat="server" />
    
    <asp:HiddenField ID="sumnotoutnum" runat="server" />
    <asp:HiddenField ID="sumnotoutamount" runat="server" />
    <asp:HiddenField ID="sumduenum" runat="server" />
    <asp:HiddenField ID="sumdueamount" runat="server" />
    
    <asp:HiddenField ID="sumrealnum" runat="server" />
    <asp:HiddenField ID="sumrealamount" runat="server" />
    <asp:HiddenField ID="sumdiffnum" runat="server" />
    <asp:HiddenField ID="sumdiffamount" runat="server" />
    
    <div class="fakeContainer">
        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound"
            EnableViewState="False">
            <HeaderTemplate>
                <table id="demoTable">
                    <tr style="background-color: #CFDCEE">
                        <td nowrap="nowrap" rowspan="3" >
                            <strong>序号</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="3">
                            <strong>存货编码</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="2" colspan="2">
                            <strong>存货名称和规格</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="3">
                            <strong>材质</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="3">
                            <strong>国标</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="3">
                            <strong>单位</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="3">
                            <strong>单价</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="2" colspan="2">
                            <strong>盘点前仓库台账账面记录</strong>
                        </td>
                        <td nowrap="nowrap" colspan="4">
                            <strong>尚未入账数量</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="2" colspan="2">
                            <strong>应结存</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="2" colspan="2">
                            <strong>盘点记录</strong>
                        </td>
                        <td nowrap="nowrap" rowspan="2" colspan="2">
                            <strong>差异记录</strong>
                        </td>
                    </tr>
                    <tr style="background-color: #CFDCEE">
                        <td nowrap="nowrap" colspan="2">
                            <strong>入库</strong>
                        </td>
                        <td nowrap="nowrap" colspan="2">
                            <strong>发出</strong>
                        </td>
                    </tr>
                    <tr style="background-color: #CFDCEE">
                        <td nowrap="nowrap">
                            <strong>存货名称</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>存货规格</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>数量</strong>
                        </td>
                        <td nowrap="nowrap">
                            <strong>金额</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Container.ItemIndex+1%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "MaterialName")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "Attribute")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "GB")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "Unit")%>
                    </td>
                    <td nowrap="nowrap">
                        <%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInAccount")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelAmountInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInAccount")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumNotIn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotIn")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelAmountNotIn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotIn")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumNotOut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotOut")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelAmountNotOut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotOut")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDueToAccount")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelAmountDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountDueToAccount")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInventory")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelAmountInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInventory")%>'></asp:Label>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelNumDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDiff")%>'></asp:Label>
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
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
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
                </tr>
                 <tr>
                    <td>
                    </td>
                    <td>
                        <strong>汇总：</strong>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
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
                </tr>
              </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel3" runat="server" Visible="false">
            没有相关记录!</asp:Panel>
    </div>
    <uc1:UCPaging ID="UCPaging3" runat="server" Visible="false" />

    <script type="text/javascript">
//<![CDATA[

var width=$("#demoTable").width();
var height=$("#demoTable").height();

if(width<=1300)
{
$("div.fakeContainer").width($("#demoTable").width()+50);
}
else
{
$("div.fakeContainer").width(1300);
}
if(height<=500)
{
$("div.fakeContainer").height($("#demoTable").height());
}
else
{
$("div.fakeContainer").height(500);
}

(function() {
	var mySt = new superTable("demoTable", {
		cssSkin : "sSky",
		fixedCols : 2,
		headerRows : 3,
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



//]]>
    </script>

    </form>
</body>
</html>
