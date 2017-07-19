<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Trans_KHZT.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_KHZT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">客户自提
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
 <link href="../StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../StyleFile/superTables_compressed.js" type="text/javascript"></script>
<div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                年份：<asp:DropDownList Width="70px" ID="DropDownListYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                (按项目工程查询)&nbsp;&nbsp;项目：<asp:DropDownList ID="DropDownListproject" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPJ_SelectedIndexChanged">
                </asp:DropDownList> &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;
                工程：<asp:DropDownList ID="DropDownListengeering" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListENG_SelectedIndexChanged">
                </asp:DropDownList> &nbsp;&nbsp;&nbsp;
            </td>
            <td align="right">
                <asp:Button ID="Add" runat="server" Text="添加"   OnClick="Add_Click"  />&nbsp;&nbsp;&nbsp;                
                <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click"  OnClientClick="javascript:return confirm('确认删除吗？');" />&nbsp;&nbsp;&nbsp;    
                <asp:Button ID="Export" runat="server" Text="导出" OnClick="Export_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <div>
     <table id="Table1" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterYZZT" runat="server" OnItemDataBound="RepeaterYZZT_ItemDataBound" >
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <th colspan="14" align="center"><strong><asp:Label ID="LabelYear" runat="server" ></asp:Label>业主自提运输台账</strong></th>                          
            </tr>
            <tr align="center">
                <th rowspan="3" nowrap><strong></strong></th>
                <th rowspan="3"  nowrap><strong>序号</strong></th>
                <th rowspan="3" nowrap><strong>项目名称</strong></th>
                <th rowspan="3" nowrap><strong>工程名称</strong></th>              
                <th rowspan="3" nowrap><strong>总重量（T）</strong></th>
                <th rowspan="3" nowrap><strong>总体积（m3）</strong></th>         
                <th rowspan="3" nowrap><strong>容重比（m3/T）</strong></th> 
                <th colspan="5" nowrap><strong>实际发运量</strong></th>
                <th rowspan="3" nowrap><strong>备注</strong></th> 
                 <th rowspan="3" nowrap></th>               
            </tr>
              <tr align="center">
               <th rowspan="2"  nowrap><strong>登记日期</strong></th>
                <th colspan="2" nowrap><strong>本期发运量</strong></th>
                 <th colspan="2" nowrap><strong>本年度累计发运量</strong></th>
              </tr>
              <tr align="center">
              <th  nowrap><strong>重量（T）</strong></th>
                <th nowrap><strong>体积（m3）</strong></th>
                <th  nowrap><strong>重量（T）</strong></th>
                <th nowrap><strong>体积（m3）</strong></th>
              </tr>
           
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                <td nowrap ><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("KHZT_ID")%>' Visible="false"></asp:Label></td>
                <td nowrap><%#Eval("KHZT_PJNAME")%></td>
                <td nowrap><%#Eval("KHZT_ENGNAME")%></td>
                <td nowrap><%#Eval("KHZT_ZW")%></td>                
                <td nowrap><%#Eval("KHZT_ZV")%></td>
                <td nowrap><%#Eval("KHZT_RZB")%></td> 
                <td nowrap><%#Eval("KHZT_DJTIME")%></td>
                <td nowrap><%#Eval("KHZT_ZL1")%></td>
                <td nowrap><%#Eval("KHZT_TJ1")%></td>                
                <td nowrap><%#Eval("KHZT_ZL2")%></td>
                <td nowrap><%#Eval("KHZT_TJ2")%></td>
                <td nowrap><%#Eval("KHZT_BZ")%></td>  
                <td nowrap><asp:HyperLink ID="HyperLinkEditKHZT" NavigateUrl='<%#"SM_Trans_KHZTEdit.aspx?FLAG=EDIT&&ID="+Eval("KHZT_ID")%>'  runat="server">
               编辑</asp:HyperLink></td>             
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td nowrap></td>
                <td nowrap><strong>合计</strong></td>
                <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
               <td nowrap></td>
             <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
                <td nowrap></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
          </table>
  </div>
  <asp:Panel ID="Panel5" runat="server" Visible="false">没有相关记录!</asp:Panel>
  <br />
    </div><!--box-outer END -->
    </div>
    
    <script language="javascript" type="text/javascript">   
        var tab=document.getElementById("table1");
var tr=tab.getElementsByTagName("tr");
if(tr.length>4)
{
    document.getElementById("table1").parentNode.className="fakeContainer";} 
        var myST = new superTable("Table1", {
	        cssSkin : "sDefault",
	        headerRows : 4,
	        fixedCols : 3,

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
   </script>   
</asp:Content>

