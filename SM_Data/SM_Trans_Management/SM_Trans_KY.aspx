<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master"  EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SM_Trans_KY.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_KY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">空运
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<link href="../StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../StyleFile/superTables_compressed.js" type="text/javascript"></script>
    
     <script language="javascript" type="text/javascript">  
     self.moveTo(0,0)     self.resizeTo(screen.availWidth,screen.availHeight)
     
   Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageloaded);
        function pageloaded(sender, args)
        {
         var tab=document.getElementById("ky");
    var tr=tab.getElementsByTagName("tr");
    if(tr.length>2)
    {
    document.getElementById("ky").parentNode.className="fakeContainer";} 
        var myST = new superTable("ky", {
	        cssSkin : "sDefault",
	        headerRows : 2,
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
        }
       
   </script>  
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="update_body" runat="server">
          <Triggers >
           <asp:PostBackTrigger ControlID="Export" />
        </Triggers>
        <ContentTemplate>
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
            <td align="right">
                <asp:Button ID="Add" runat="server" Text="添加" OnClick="Add_Click" />&nbsp;&nbsp;&nbsp;                
                <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" OnClientClick="javascript:return confirm('确认删除吗？');" />&nbsp;&nbsp;&nbsp;                     
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
    <table id="ky" width="100%" align="center" cellpadding="4" cellspacing="1" border="1">
        <asp:Repeater ID="RepeaterKY" runat="server" OnItemDataBound="RepeaterKY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" >
                <th colspan="15"><strong><asp:Label ID="LabelYear" runat="server" ></asp:Label>年货物空运发运台账</strong></th>                          
            </tr>
            <tr align="center">
                <th nowrap><strong></strong></th>
                <th nowrap><strong>序号</strong></th>
                <th nowrap><strong>项目名称</strong></th>
                <th nowrap><strong>货物名称</strong></th>
                <th nowrap><strong>件数</strong></th>
                <th nowrap><strong>包装形式</strong></th>
                <th nowrap><strong>体积（m3）</strong></th>
                <th nowrap><strong>重量（KG）</strong></th>
                <th nowrap><strong>运费（元）</strong></th>
                <th nowrap><strong>运输公司</strong></th>
                <th nowrap><strong>发运日期</strong></th>
                <th nowrap><strong>发运人</strong></th>
                <th nowrap><strong>备注</strong></th>
                <th nowrap><strong>运费结算情况</strong></th>
                <th nowrap><strong>编辑</strong></th>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                <td nowrap><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("KYID")%>' Visible="false"></asp:Label></td>
                <td nowrap><%#Eval("KYPROJECT")%></td>
                <td nowrap><%#Eval("KYGOODNAME")%></td>
                <td nowrap><%#Eval("KYNUM")%></td>                
                <td nowrap><%#Eval("KYBZXS")%></td>
                <td nowrap><%#Eval("KYTJ")%></td>
                <td nowrap><%#Eval("KYZL")%></td>
                <td nowrap><%#Eval("KYYF")%></td>
                <td nowrap><%#Eval("KYYSGS")%></td>
                <td nowrap><%#Eval("KYTRANSDATE")%></td>
                <td nowrap><%#Eval("KYFYR")%></td>
                <td nowrap><%#Eval("KYBZ")%></td>
                <td nowrap><%#Eval("KYYFJSQK")%></td>
                <td nowrap>
                <asp:HyperLink ID="HyperLinkEditKY" NavigateUrl='<%#"SM_Trans_KYEdit.aspx?FLAG=EDIT&&ID="+Eval("KYID")%>'  runat="server">
<%--                <asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
--%>                编辑</asp:HyperLink>
                </td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td>&nbsp;</td>                
                <td nowrap><strong>合计</strong></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>                       
            </tr>
            </FooterTemplate>
        </asp:Repeater>        
    </table>
   </div>
   <asp:Panel ID="Panel7" runat="server" Visible="false">没有相关记录!</asp:Panel>
    <br />
    </div><!--box-outer END -->
    </div> 
     </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>