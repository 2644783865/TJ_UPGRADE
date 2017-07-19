<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true"
    CodeBehind="CM_ServiceDet.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    售后服务登记</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <JSR:JSRegister ID="JSRegister1" runat="server" />
    <script language="javascript" type="text/javascript">
        function add(number) {
            var htbh = document.getElementById("<%=txt_htbh.ClientID%>").value;
            window.open("CM_shfwwj_Detail.aspx?action=add&type=" + number + "&dh=" + htbh + "");
        }
        function autoHtxx() {
            var htbh = document.getElementById("<%=txt_htbh.ClientID%>").value;
            var conn = new ActiveXObject("adodb.connection")
            var connstr = GetConnection();
            conn.open(connstr);
            var comText = "select SH_HTMC,SH_XMMC,SH_GCMC,SH_KHMC,SH_FWNR,CONVERT(varchar(100), SH_JHSJ, 23) AS SH_JHSJ,CONVERT(varchar(100), SH_JSSJ, 23) AS SH_JSSJ,SH_FWR from CM_SHFWSQ where SH_HTBH='" + htbh + "' ";
            var objrs = conn.Execute(comText);
            if (!objrs.EOF) {
                document.getElementById("<%=txt_htmc.ClientID %>").value = objrs.Fields(0).value;
                document.getElementById("<%=txt_xmmc.ClientID %>").value = objrs.Fields(1).value;
                document.getElementById("<%=txt_gcmc.ClientID %>").value = objrs.Fields(2).value;
                document.getElementById("<%=txt_khmc.ClientID %>").value = objrs.Fields(3).value;
                document.getElementById("<%=txt_khfwnr.ClientID %>").value = objrs.Fields(4).value;
                document.getElementById("<%=kssj.ClientID %>").value = objrs.Fields(5).value;
                document.getElementById("<%=jssj.ClientID %>").value = objrs.Fields(6).value;
                document.getElementById("<%=txt_fwry.ClientID %>").value = objrs.Fields(7).value;
            }
            else {
                alert("请输入正确的合同编号！");
                return;
            }
            objrs.close();
            conn.close();
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField runat="server" ID="UserName" />
<div class="RightContent">
    <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
    <asp:Button ID="btnLoad" runat="server" 
            Text="保 存"  CssClass="button-outer" onclick="btnLoad_Click"/> 
                                &nbsp;&nbsp; 
    <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="False"  CssClass="button-outer" 
                                    onclick="btnReturn_Click"/>
                                &nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
</div>

<div style="height:8px" class="box_top"></div>
     <div class="box-wrapper">
        <div class="box-outer">
        
         <table width="100%">
         <tr>
         <td style="width:10px">
            <asp:Image ID="Image10" style="CURSOR: hand"  ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif" onClick="switchGridVidew(this,'htjbxx')" Height="15" Width="15" runat="server" /></td>
         <td>售后服务基本信息</td>
         </table>
<div id="htjbxx" style="display:block;">
<Table ID="tab1" runat="server" cellpadding="4" cellspacing="1" runat="server" class="toptable grid" border="1">
<tr>
<td style="width: 70px">合同编号：</td>
<td style="width: 484px"><asp:TextBox ID="txt_htbh" runat="server" Width="200px" onblur="autoHtxx(this)"></asp:TextBox>
<span id="span1" runat="server" visible="false" class="Error">*</span>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写合同编号" ControlToValidate="txt_htbh" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:AutoCompleteExtender ID="htbh_AutoCompleteExtender" runat="server" CompletionSetCount="10" DelimiterCharacters="" Enabled="true"
 MinimumPrefixLength="1" ServiceMethod="SHHtCode" ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txt_htbh" UseContextKey="true"></asp:AutoCompleteExtender>
</td>
<td visible="false"><asp:TextBox ID="dh" runat="server" Visible="false"></asp:TextBox></td>
<td style="width: 113px">合同名称：</td>
<td><asp:TextBox ID="txt_htmc" runat="server" Width="200px"></asp:TextBox></td>
</tr>
<tr>
<td style="width: 70px">项目名称：</td>
<td style="width: 484px"><asp:TextBox ID="txt_xmmc" runat="server" Width="200px"></asp:TextBox></td>
<td style="width: 113px">工程名称：</td>
<td><asp:TextBox ID="txt_gcmc" runat="server" Width="200px"></asp:TextBox></td>
</tr>
<tr>
<td style="width: 70px">业主名称：</td>
<td style="width: 484px"><asp:TextBox ID="txt_khmc" runat="server" Width="200px"></asp:TextBox></td>
<td style="width: 113px">主要服务内容：</td>
<td><asp:TextBox ID="txt_khfwnr" runat="server" Width="200px"></asp:TextBox></td>
</tr>
<tr>
<td style="width: 70px">服务人员：</td>
<td style="width: 484px"><asp:TextBox ID="txt_fwry" runat="server" Width="200px"></asp:TextBox></td>
<td style="width: 113px">合同要求服务时间:</td>
<td><input id="yqfwsj" runat="server" type="text" onclick="setday(this)" style="width:200px"/></td>
</tr>
<tr>
<td style="width: 70px">开始时间：</td>
<td style="width: 484px"><input id="kssj" runat="server" type="text" onclick="setday(this)" style="width:200px"/></td>
<td style="width: 113px">结束时间：</td>
<td><input id="jssj" runat="server" type="text" onclick="setday(this)" style="width:200px" /></td>
</tr>
<tr>
<td style="width: 70px">商务人员：</td>
<td style="width: 484px"><asp:Label ID="swry" runat="server"></asp:Label></td>
<td style="width: 113px">结算情况：</td>
<td><asp:DropDownList ID="drp_jsqk" runat="server">
<asp:ListItem Text="无(不需要)" Value="0"></asp:ListItem>
<asp:ListItem Text="未完成" Value="1"></asp:ListItem>
<asp:ListItem Text="已完成" Value="2"></asp:ListItem>
</asp:DropDownList></td>
</tr>
<tr>
<td style="width: 70px">备注：</td>
<td colspan="2"><asp:TextBox ID="bz" runat="server" TextMode="MultiLine" Width="400px" Height="150px"></asp:TextBox></td>
<td><span id="message" visible="false" class="Error" runat="server">删除确认前，请删除相应的登记信息及附件，以免造成数据库与服务器的压力！</span></td>
</tr>
</Table>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="1">
<asp:TabPanel id="Tab_MYD" runat="server" TabIndex="0" HeaderText="客户满意度调查表">
<ContentTemplate>
<div class="box-inner"><div class="box_right">
    <div class="box-title">
<table width="100%">
<tr>
<td align="right"><asp:Button ID="djwj1" runat="server" Text="登记文件" OnClientClick="add(0)" /></td>
</tr>
</table>
</div>
</div></div>

<div class="box-wrapper">      
<div class="box-outer">
<asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
    <Columns>
        <asp:TemplateField HeaderText="序号">
        <ItemTemplate>
        <asp:Label ID="xh1" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="WJ_HTBH" HeaderText="合同编号">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_HTMC" HeaderText="合同名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_XMMC" HeaderText="项目名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_GCMC" HeaderText="工程名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_KHMC" HeaderText="客户单位">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="商务人员">
        <ItemTemplate>
        <asp:Label ID="zdr" runat="server" Text='<%#Eval("WJ_SQR") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="修改">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink1" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=mod&type=0&id="+Eval("WJ_DH") %>' Target="_blank">
        <asp:Image ID="image1" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            修改
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="查看">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink2" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=view&type=0&id="+Eval("WJ_DH") %>' Target="_blank">
        <asp:Image ID="image2" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            查看
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink3" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=del&type=0&id="+Eval("WJ_DH") %>' Target="_blank">
        <asp:Image ID="image3" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            删除
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />   
  <RowStyle BackColor="White" />
    </asp:GridView>
    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
<uc1:UCPaging ID="UCPaging1" runat="server" />
</div>
</div>
    </ContentTemplate>
</asp:TabPanel>

<asp:TabPanel ID="Tab_ZJBG" runat="server" TabIndex="1" HeaderText="总结报告">
<ContentTemplate>
<div class="box-inner"><div class="box_right">
    <div class="box-title">
<table width="100%">
<tr>
<td align="right"><asp:Button ID="djwj2" runat="server" Text="登记文件" OnClientClick="add(1)" /></td>
</tr>
</table>
</div>
</div></div>

<div class="box-wrapper">      
<div class="box-outer">
<asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
    <Columns>
        <asp:TemplateField HeaderText="序号">
        <ItemTemplate>
        <asp:Label ID="xh2" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
       <asp:BoundField DataField="WJ_HTBH2" HeaderText="合同编号">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_HTMC2" HeaderText="合同名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_XMMC2" HeaderText="项目名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_GCMC2" HeaderText="工程名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_KHMC2" HeaderText="客户单位">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
               <asp:TemplateField HeaderText="商务人员">
        <ItemTemplate>
        <asp:Label ID="zdr2" runat="server" Text='<%#Eval("WJ_SQR2") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="修改">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink4" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=mod&type=1&id="+Eval("WJ_DH2") %>' Target="_blank">
        <asp:Image ID="image4" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            修改
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="查看">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink5" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=view&type=1&id="+Eval("WJ_DH2") %>' Target="_blank">
        <asp:Image ID="image5" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            查看
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink6" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=del&type=1&id="+Eval("WJ_DH2") %>' Target="_blank">
        <asp:Image ID="image6" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            删除
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />   
  <RowStyle BackColor="White" />
    </asp:GridView>
    <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
<uc1:UCPaging ID="UCPaging2" runat="server" />
</div>
</div>
    </ContentTemplate>
</asp:TabPanel>

<asp:TabPanel ID="Tab_ZGZBG" runat="server" TabIndex="2" HeaderText="周工作报告">
<ContentTemplate>
<div class="box-inner"><div class="box_right">
    <div class="box-title">
<table width="100%">
<tr>
<td align="right"><asp:Button ID="djwj3" runat="server" Text="登记文件" OnClientClick="add(2)" /></td>
</tr>
</table>
</div>
</div></div>

<div class="box-wrapper">      
<div class="box-outer">
<asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
    <Columns>
        <asp:TemplateField HeaderText="序号">
        <ItemTemplate>
        <asp:Label ID="xh3" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="WJ_HTBH3" HeaderText="合同编号">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_HTMC3" HeaderText="合同名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_XMMC3" HeaderText="项目名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_GCMC3" HeaderText="工程名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_KHMC3" HeaderText="客户单位">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
                <asp:TemplateField HeaderText="商务人员">
        <ItemTemplate>
        <asp:Label ID="zdr3" runat="server" Text='<%#Eval("WJ_SQR3") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="修改">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink7" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=mod&type=2&id="+Eval("WJ_DH3") %>' Target="_blank">
        <asp:Image ID="image1" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            修改
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="查看">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink8" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=view&type=2&id="+Eval("WJ_DH3") %>' Target="_blank">
        <asp:Image ID="image2" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            查看
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink9" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=del&type=2&id="+Eval("WJ_DH3") %>' Target="_blank">
        <asp:Image ID="image3" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            删除
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />   
  <RowStyle BackColor="White" />
    </asp:GridView>
    <asp:Panel ID="NoDataPanel3" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
<uc1:UCPaging ID="UCPaging3" runat="server" />
</div>
</div>
    </ContentTemplate>
</asp:TabPanel>

<asp:TabPanel ID="Tab_KQB" runat="server" TabIndex="3" HeaderText="考勤表">
<ContentTemplate>
<div class="box-inner"><div class="box_right">
    <div class="box-title">
<table width="100%">
<tr>
<td align="right"><asp:Button ID="djwj4" runat="server" Text="登记文件" OnClientClick="add(3)" /></td>
</tr>
</table>
</div>
</div></div>

<div class="box-wrapper">      
<div class="box-outer">
<asp:GridView ID="GridView4" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
    <Columns>
        <asp:TemplateField HeaderText="序号">
        <ItemTemplate>
        <asp:Label ID="xh4" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
         <asp:BoundField DataField="WJ_HTBH4" HeaderText="合同编号">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_HTMC4" HeaderText="合同名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_XMMC4" HeaderText="项目名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_GCMC4" HeaderText="工程名称">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="WJ_KHMC4" HeaderText="客户单位">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="商务人员">
        <ItemTemplate>
        <asp:Label ID="zdr4" runat="server" Text='<%#Eval("WJ_SQR4") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    
        <asp:TemplateField HeaderText="修改">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink10" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=mod&type=3&id="+Eval("WJ_DH4") %>' Target="_blank">
        <asp:Image ID="image1" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            修改
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="查看">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink11" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=view&type=3&id="+Eval("WJ_DH4") %>' Target="_blank">
        <asp:Image ID="image2" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            查看
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <asp:HyperLink ID="hyperlink12" runat="server" CssClass="link" NavigateUrl='<%#"CM_shfwwj_Detail.aspx?action=del&type=3&id="+Eval("WJ_DH4") %>' Target="_blank">
        <asp:Image ID="image3" runat="server"  border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
            删除
        </asp:HyperLink>
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />   
  <RowStyle BackColor="White" />
    </asp:GridView>
    <asp:Panel ID="NoDataPanel4" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
<uc1:UCPaging ID="UCPaging4" runat="server" />
</div>
</div>
    </ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
</div>
</contenttemplate>
</asp:Content>
