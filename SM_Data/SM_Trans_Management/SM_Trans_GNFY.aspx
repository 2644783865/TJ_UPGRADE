<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SM_Trans_GNFY.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_GNFY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">国内发运
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
        var tab=document.getElementById("gnfy");
        var tr=tab.getElementsByTagName("tr");
        if(tr.length>4)
        {
        document.getElementById("gnfy").parentNode.className="fakeContainer";} 
        var myST = new superTable("gnfy", {
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
                年份：<asp:DropDownList ID="DropDownListYear" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
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
    <table id="gnfy" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterGNFY" runat="server" OnItemDataBound="RepeaterGNFY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <th colspan="19"><strong><asp:Label ID="LabelYear" runat="server" ></asp:Label>国内设备发运台账</strong></th>                          
            </tr>
            <tr align="center">
                <th rowspan="3" ><strong></strong></th>
                <th rowspan="3" nowrap><strong>序号</strong></th>
                <th rowspan="3" nowrap><strong>项目名称</strong></th>
                <th rowspan="3" nowrap><strong>工程名称</strong></th>
                <th colspan="4" nowrap><strong>运输合同</strong></th>
                <th rowspan="3"  nowrap><strong>容重比（m3/T）</strong></th>
                <th colspan="6" nowrap><strong>实际发运量</strong></th>
                <th rowspan="3" nowrap><strong>过磅重量（T）</strong></th>
                <th rowspan="3" nowrap><strong>最终结算金额（元）</strong></th>
                <th rowspan="3" nowrap><strong>备注</strong></th>
                <th rowspan="3" nowrap></th>
                </tr>
            <tr align="center">
                <th rowspan="2" nowrap><strong>合同编号</strong></th>
                <th rowspan="2" nowrap><strong>合同金额</strong></th>
                <th rowspan="2" nowrap><strong>总重量（T）</strong></th>
                <th rowspan="2" nowrap><strong>总体积（m3）</strong></th>
                <th rowspan="2" nowrap><strong>登记日期</strong></th>
                <th colspan="2" nowrap><strong>本期发运量</strong></th>
                <th colspan="2" nowrap><strong>年度累计发运量</strong></th>
                <th  rowspan="2" nowrap><strong>车次</strong></th>
                </tr>
                <tr align="center">
                <th nowrap><strong>质量（T）</strong></th>
                <th nowrap><strong>体积（m3）</strong></th>
                <th nowrap><strong>质量（T）</strong></th>
                <th nowrap><strong>体积（m3）</strong></th>
                </tr>
                
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td nowrap><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("GNFY_ID")%>' Visible="false"></asp:Label></td>
                <td nowrap><%#Eval("GNFY_PJNAME")%></td>
                <td nowrap><%#Eval("GNFY_ENGNAME")%></td>
                <td nowrap><%#Eval("GNFY_HTBH")%></td> 
                 <td nowrap><%#Eval("GNFY_HTJE")%></td>               
                <td nowrap><%#Eval("GNFY_ZW")%></td>
                <td nowrap><%#Eval("GNFY_ZV")%></td>
                <td nowrap><%#Eval("GNFY_RZB")%></td>
                <td nowrap><%#Eval("GNFY_DJTIME")%></td>
                <td nowrap><%#Eval("GNFY_ZL1")%></td>
                <td nowrap><%#Eval("GNFY_TJ1")%></td>
                <td nowrap><%#Eval("GNFY_ZL2")%></td>
                <td nowrap><%#Eval("GNFY_TJ2")%></td>
                <td nowrap><%#Eval("GNFY_CC")%></td>
                <td nowrap><%#Eval("GNFY_GBZL")%></td>
                <td nowrap><%#Eval("GNFY_LJE")%></td>
                <td nowrap><%#Eval("GNFY_BZ")%></td>
                               
                <td nowrap><asp:HyperLink ID="HyperLinkEditGNFY" NavigateUrl='<%#"SM_Trans_GNFYEdit.aspx?FLAG=EDIT&&ID="+Eval("GNFY_ID")%>'  runat="server">
<%--                <asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
--%>                编辑</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>                
                <td><strong>合计</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                        
            </tr>
            </FooterTemplate>
        </asp:Repeater>
    </table>
   </div>
   <asp:Panel ID="Panel4" runat="server" Visible="false">没有相关记录!</asp:Panel>
   <br />
   </div><!--box-outer END -->
    </div>
</ContentTemplate>
</asp:UpdatePanel>
 
</asp:Content>
