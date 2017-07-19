<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SM_Trans_JZXFY.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_JZXFY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">集装箱发运
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
        var tab=document.getElementById("jzxfy");
        var tr=tab.getElementsByTagName("tr");
        if(tr.length>3)
        {
        document.getElementById("jzxfy").parentNode.className="fakeContainer";} 
        var myST = new superTable("jzxfy", {
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
                (按项目查询)&nbsp;&nbsp;项目：<asp:DropDownList ID="DropDownListproject" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPJ_SelectedIndexChanged">
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
    <table id="jzxfy" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterJZXFY" runat="server" OnItemDataBound="RepeaterJZXFY_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">
                <th colspan="16"><strong>中材重机储运部<asp:Label ID="LabelYear" runat="server" ></asp:Label>度集装箱发运统计</strong></th>                          
            </tr>
            <tr align="center">
                <th rowspan="2" nowrap><strong></strong></th>
                <th rowspan="2" nowrap><strong>序号</strong></th>
                <th rowspan="2" nowrap><strong>项目名称</strong></th>
                <th rowspan="2" nowrap><strong>发运批次</strong></th>
                <th rowspan="2" nowrap><strong>发运日期</strong></th>
                <th rowspan="2" nowrap><strong>货物描述</strong></th>
                <th colspan="3" nowrap><strong>装货量</strong></th>
                <th rowspan="2" nowrap><strong>容重比</strong></th>
                <th rowspan="2" nowrap><strong>体积装箱率</strong></th>
                <th rowspan="2" nowrap><strong>重量装箱率</strong></th>
                <th rowspan="2" nowrap><strong>箱型及箱数</strong></th>
                <th rowspan="2" nowrap><strong>装箱所用材料</strong></th>
                <th rowspan="2" nowrap><strong>添加明细</strong></th>                
                <th rowspan="2" nowrap><strong>编辑</strong></th>
            </tr>
            <tr align="center">
                <th nowrap><strong>箱数</strong></th>
                <th nowrap><strong>立方米</strong></th>
                <th nowrap><strong>货重（T）</strong></th>  
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>                
                <td nowrap><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("JZXFYID")%>' Visible="false"></asp:Label></td>
                <td nowrap><%#Eval("JZXFYPROJECT")%></td>
                <td nowrap><%#Eval("JZXFYFYPC")%></td>
                <td nowrap><%#Eval("JZXFYTRANSDATE")%></td>                
                <td nowrap><%#Eval("JZXFYHWMS")%></td>
                <td nowrap><%#Eval("JZXFYXS")%></td>
                <td nowrap><%#Eval("JZXFYLFM")%></td>
                <td nowrap><%#Eval("JZXFYHZ")%></td>
                <td nowrap><%#Eval("JZXFYRZB")%></td>
                <td nowrap><%#Eval("JZXFYTJZXL")%></td>
                <td nowrap><%#Eval("JZXFYZLZXL")%></td>
                <td nowrap><%#Eval("JZXFYXXJXS")%></td>
                <td nowrap><%#Eval("JZXFYZXSYCL")%></td>
                <td nowrap><asp:HyperLink ID="HyperLinkEditJZXFYMX" NavigateUrl='<%#"SM_Trans_JZXFYMXEdit.aspx?FLAG=NEW&&ID=NEW&&PID="+Eval("JZXFYID")%>'  runat="server"><asp:Image ID="ImageAddDetail" ImageUrl="~/assets/icons/add.gif" border="0" hspace="2" align="absmiddle" runat="server" />添加明细</asp:HyperLink></td>
                <td nowrap><asp:HyperLink ID="HyperLinkEditJZXFY" NavigateUrl='<%#"SM_Trans_JZXFYEdit.aspx?FLAG=EDIT&&ID="+Eval("JZXFYID")%>'  runat="server">
<%--                <asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
--%>                编辑</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle">
                <td></td>
                <td nowrap><strong>合计：</strong></td>
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
   <asp:Panel ID="Panel9" runat="server" Visible="false">没有相关记录!</asp:Panel>
   <br />
    </div><!--box-outer END -->
    </div> 
     </ContentTemplate>
</asp:UpdatePanel>     
</asp:Content>