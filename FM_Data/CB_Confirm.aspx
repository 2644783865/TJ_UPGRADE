<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="CB_Confirm.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CB_Confirm" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
function show(id)
{
  var date=new Date();  var time=date.getTime();  var sRet=window.showModalDialog("CB_Confirm_detail.aspx?id="+id+"&NoUse="+time,"obj","dialogWidth=500px;dialogHeight=400px;status:no;");
  if(sRet=="refresh")
  { 
    window.location.reload();
  }
}

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="center">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                                <asp:ListItem Text="待确定" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已确定" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已完成" Value="2" ></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            按部门查询:<asp:DropDownList ID="ddlDep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                                <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                <asp:ListItem Text="技术部" Value="03"></asp:ListItem>
                                <asp:ListItem Text="市场部" Value="12"></asp:ListItem>
                                <asp:ListItem Text="储运部" Value="07"></asp:ListItem>
                                <asp:ListItem Text="生产部" Value="04"></asp:ListItem>
                                <asp:ListItem Text="采购部" Value="06"></asp:ListItem>
                                <asp:ListItem Text="电器制造部" Value="09"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            请选择查询类型：<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                                <asp:ListItem Text="-请选择-" Value="请选择"></asp:ListItem>
                                <asp:ListItem Text="生产制号" Value="1"></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="2"></asp:ListItem>
                                <asp:ListItem Text="工程名称" Value="3"></asp:ListItem>
                                 </asp:DropDownList>
                            <asp:TextBox ID="txtTaskID" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_OnClick" Text="查 询" />
                        </td>
                        <td>
                        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Onclick" Text="导出" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="Rbm" runat="server">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle">
                                <th rowspan="2">
                                    <strong>序号</strong>
                                </th>
                                <th rowspan="2">
                                    <strong>生产制号</strong>
                                </th>
                                <th rowspan="2">
                                    <strong>项目名称</strong>
                                </th>
                                <th rowspan="2">
                                    <strong>工程名称</strong>
                                </th>
                                <th rowspan="2">
                                    <strong>确认完工日期</strong>
                                </th>
                                <%--<th rowspan="2">
                                    <strong>当前状态</strong>
                                </th>--%>
                                <th colspan="2">
                                    <strong>技术部</strong>
                                </th>
                                <th colspan="5">
                                    <strong>市场部</strong>
                                </th>
                                <th colspan="3">
                                    <strong>储运部</strong>
                                </th>
                                <th>
                                    <strong>生产部</strong>
                                </th>
                                <th>
                                    <strong>采购部</strong>
                                </th>
                                <th><strong>电器制造部</strong></th>
                            </tr>
                            <tr align="center" class="tableTitle">
                            <th align="center">
                                    <strong>是否发运</strong>
                                </th>
                                <th align="center">
                                    <strong>委外是否结算完毕</strong>
                                </th>
                                <th align="center">
                                    <strong>合同号</strong>
                                </th>
                                <th align="center">
                                    <strong>单位</strong>
                                </th>
                                <th align="center">
                                    <strong>结算量</strong>
                                </th>
                                <th align="center">
                                    <strong>单价</strong>
                                </th>
                                <th align="center">
                                    <strong>结算金额</strong>
                                </th>
                                
                                <th align="center">
                                    <strong>是否发运</strong>
                                </th>
                                <th align="center">
                                    <strong>运输发票是否结算</strong>
                                </th>
                                <th align="center">
                                    <strong>发料是否都录入出库系统</strong>
                                </th>
                                <th align="center">
                                    <strong>外协是否结算</strong>
                                </th>
                                <th align="center">
                                    <strong>是否结算</strong>
                                </th>
                                <th align="center" ><strong>是否结算</strong></th>  
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" onclick="this.className='clickback'" onmouseout="this.className='baseGadget'"
                                ondblclick='javascript:show("<%#Eval("TASK_ID") %>")'>
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("TASK_ID")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("PRJ")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("ENG")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("WGRQ")%>&nbsp;
                                </td>
                                <%--<td>
                                    <%#Eval("STATUS").ToString()=="0"?"待确认":Eval("STATUS").ToString()=="1"?"待核算":Eval("STATUS").ToString()=="2"?"已确定":Eval("STATUS").ToString()=="3"?"核算中":"已核算" %>
                                </td>--%>
                                 <td>
                                    <%#Eval("JFY")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("JWW")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("HT_ID")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("DANWEI")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("JSL")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("DANJIA")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("PRICE")%>&nbsp;
                                </td>
                               
                                <td>
                                    <%#Eval("CFY")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("CFP")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("CCRK")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("SJS")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("CGFP")%>&nbsp;
                                </td>
                               <td><%#Eval("DQFP")%>&nbsp;</td> 
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <br />
                <asp:Panel ID="NoDataPanel" HorizontalAlign="Center" runat="server" Visible="false">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />

            <script language="javascript" type="text/javascript">  
    var tab=document.getElementById("table1");
var tr=tab.getElementsByTagName("tr");
var ss=tr.length;
if(tr.length>2)
{
    document.getElementById("table1").parentNode.className="fakeContainer";
        var myST = new superTable("table1", {
	        cssSkin : "sDefault",
	        headerRows : 2,
	        fixedCols : 4,

	        onStart : function () 
	        {

	        },
	        onFinish : function () 
	        {
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
                                dataRow.style.backgroundColor = "#ffffff";
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
             return this;
            }
        });
 }       
            </script>

        </div>
    </div>
</asp:Content>
