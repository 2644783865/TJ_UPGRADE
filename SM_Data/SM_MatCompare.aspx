<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="SM_MatCompare.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_MatCompare" Title="物料比对" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   物料比对
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

<script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
 var postBack=true;

function EndRequestHandler(sender, args){
if(postBack){
    document.getElementById("GridView1").parentNode.className = "fakeContainer";

    (function() {
        superTable("GridView1", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
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
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#e4ecf7";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#BFDFFF";
                                        fixedRow.style.backgroundColor = "#409FFF";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
                   }
        });
    })();
    
ActivateAlertDiv('hidden', 'AlertDiv', '');
}
postBack=true;
 }


    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="hfdtn" runat="server" />
                <asp:HiddenField ID="hfdtp" runat="server" />
                <table width="98%">
                    <tr>
                        <td align="left">
                           &nbsp;&nbsp;&nbsp;
                           原任务号：
                           <input type="text" id="yuantsaid" runat="server" />
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           比对任务号：
                           <input type="text" id="biduitsaid" runat="server" />
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           数量关系：
                           <asp:DropDownList ID="drprelation" runat="server">
                              <asp:ListItem Value="0" Text="全部"></asp:ListItem>
                              <asp:ListItem Value="1" Text="原任务号大于等于比对任务号"></asp:ListItem>
                              <asp:ListItem Value="2" Text="原任务号小于比对任务号"></asp:ListItem>
                           </asp:DropDownList>
                           &nbsp;&nbsp;&nbsp;
                           <input type="button" id="btntsaidcompare" runat="server" value="物料比对" onserverclick ="btntsaidcompare_click" />
                        </td>
                        <td align="right">
                           <input type="button" id="btnexport" runat="server" value="导出数据" onserverclick="btnexport_click" />
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                   
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            没有相关物料信息!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            原任务号
                                        </td>
                                        <td>
                                            比对任务号
                                        </td>
                                        <td>
                                            部件图号(部件名称)
                                        </td>  
                                        <td>
                                            物料编码
                                        </td>
                                        <td>
                                            物料名称
                                        </td>
                                        <td>
                                            规格型号
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            国标
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            数量(原任务号)
                                        </td>
                                        <td>
                                            张(支)(原任务号)
                                        </td>
                                        <td>
                                            数量(比对任务号)
                                        </td>
                                        <td>
                                            张(支)(比对任务号)
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                        </td>
                                        <td>
                                            <%#Eval("yuanengid")%>
                                        </td>
                                        <td>
                                            <%#Eval("biduiengid")%>
                                        </td>
                                        <td>
                                            <%#Eval("bujianmapname")%>
                                        </td>
                                        <td>
                                            <%#Eval("marid")%>
                                        </td>
                                        <td>
                                            <%#Eval("marname")%>
                                        </td>
                                        <td>
                                            <%#Eval("guige")%>
                                        </td>
                                        <td>
                                            <%#Eval("caizhi")%>
                                        </td>
                                        <td>
                                            <%#Eval("guobiao")%>
                                        </td>
                                        <td>
                                            <%#Eval("length")%>
                                        </td>
                                        <td>
                                            <%#Eval("width")%>
                                        </td>
                                        <td>
                                            <%#Eval("unit")%>
                                        </td>
                                        <td>
                                            <%#Eval("ynum")%>
                                        </td>
                                        <td>
                                            <%#Eval("yfznum")%>
                                        </td>
                                        <td>
                                            <%#Eval("bdnum")%>
                                        </td>
                                        <td>
                                            <%#Eval("bdfznum")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />

                    <script type="text/javascript">
  
document.getElementById("GridView1").parentNode.className = "fakeContainer";

(function() {
    superTable("GridView1", {
        cssSkin : "Default",
        fixedCols : 2,
        onFinish : function () 
        {  
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
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
                              
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                clicked = false;
                             
                            }
                            else 
                            {
                              if( fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                              {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                fixedRow.style.backgroundColor = "#409FFF";
                                 
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                clicked = true;
                              }
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();


                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="AlertDiv" class="AlertStyle">
                <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
            </div>
        </div>
    </div>
</asp:Content>
