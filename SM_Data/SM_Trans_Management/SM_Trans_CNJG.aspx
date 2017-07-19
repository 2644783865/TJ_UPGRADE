<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SM_Trans_CNJG.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_CNJG" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">厂内集港
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
 <script language="javascript" type="text/javascript">  
     self.moveTo(0,0)     self.resizeTo(screen.availWidth,screen.availHeight)
     
   Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageloaded);
        function pageloaded(sender, args)
        {
          var tab=document.getElementById("cnjg");
        var tr=tab.getElementsByTagName("tr");
        if(tr.length>3)
        {
        document.getElementById("cnjg").parentNode.className="fakeContainer";} 
        var myST = new superTable("cnjg", {
	        cssSkin : "sDefault",
	        headerRows : 3,
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
    
    <link href="../StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <script src="../StyleFile/superTables_compressed.js" type="text/javascript"></script>
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

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
               </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                开始(结束)月份：<asp:DropDownList ID="DropDownMonth" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
               </asp:DropDownList>&nbsp;&nbsp;&nbsp;
               
            </td>
            <td align="right">
                <asp:Button ID="Add" runat="server" Text="添加" OnClick="Add_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click"  OnClientClick="javascript:return confirm('确认删除吗？');"/>&nbsp;&nbsp;&nbsp;  
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
    <table id="cnjg" width="100%" align="center" cellpadding="4" cellspacing="1"  border="1">
        <asp:Repeater ID="RepeaterCNJG" runat="server" OnItemDataBound="RepeaterCNJG_ItemDataBound">
            <HeaderTemplate>
            <tr align="center">
                <th colspan="20"><strong><asp:Label ID="LabelYear" runat="server"></asp:Label>集港发运台账</strong></th>                          
            </tr>
            <tr align="center" >
                <th rowspan="2"><strong></strong></th>
                <th rowspan="2" nowrap><strong>序号</strong></th>
                <th rowspan="2"><strong>船次/批次</strong></th>
                <th rowspan="2"><strong>目的港</strong></th>
                <th rowspan="2"><strong>理论容重比（立方米/吨）</strong></th>
                <th><strong>合同</strong></th>
                <th colspan="7"><strong>理论</strong></th>
                <th colspan="3"><strong>过磅结算</strong></th>
                <th rowspan="2"><strong>中材建设（吨）</strong></th>
                <th rowspan="2"><strong>中材建设部分金额</strong></th>
                <th rowspan="2"><strong>备注</strong></th>
                <th rowspan="2" nowrap><strong>编辑</strong></th>
            </tr>
            <tr align="center" >
                <th><strong>合同编号</strong></th>
                <th><strong>开始日期</strong></th>
                <th><strong>结束日期</strong></th>
                <th><strong>非大件重量（T）</strong></th>
                <th><strong>大件重量（T）</strong></th>
                <th><strong>总体积（m3）</strong></th>
                <th><strong>总重量（T）</strong></th>
                <th><strong>车次（T）</strong></th>                   
                <th><strong>非大件重量（T）</strong></th>
                <th><strong>大件重量</strong></th>                   
                <th><strong>金额（元）</strong></th>                                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("CNJGID")%>' Visible="false"></asp:Label></td>
                <td><%#Eval("CNJGCC")%></td>
                <td><%#Eval("CNJGMDG")%></td>
                <td><%#Eval("CNJGRZB")%></td>                
                <td><%#Eval("CNJGHTH")%></td>
                <td><%#Eval("CNJGLLSTARTDATE")%></td>
                <td><%#Eval("CNJGLLENDDATE")%></td>
                <td><%#Eval("CNJGLLFDJZL")%></td>
                <td><%#Eval("CNJGLLDJZL")%></td>
                <td><%#Eval("CNJGLLZTJ")%></td>
                <td><%#Eval("CNJGLLZZL")%></td>
                <td><%#Eval("CNJGLLCC")%></td>
                <td><%#Eval("CNJGGBFDJZL")%></td>
                <td><%#Eval("CNJGGBDJZL")%></td>
                <td><%#Eval("CNJGGBJE")%></td>
                <td><%#Eval("CNJGZCJSZL")%></td>
                <td><%#Eval("CNJGZCJSJE")%></td>
                <td><%#Eval("CNJGBZ")%></td>                
                <td><asp:HyperLink ID="HyperLinkEditCNJG" NavigateUrl='<%#"SM_Trans_CNJGEdit.aspx?FLAG=EDIT&&ID="+Eval("CNJGID")%>'  runat="server">
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
                <td></td>           
            </tr>
            </FooterTemplate>
        </asp:Repeater>        
    </table>    
    </div>
    <asp:Panel ID="nodatapal" runat="server" >没有相关记录！</asp:Panel>
    <br />
    </div><!--box-outer END -->
    </div> 
     
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>