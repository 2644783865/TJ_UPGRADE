<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_WorkAmount.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_WorkAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工程量信息           
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<script language="javascript" type="text/javascript">

    function openLink(url)
    {
        var returnValue=window.showModalDialog("TM_Engineering_Reg.aspx?fag=look&register=" + url,'',"dialogHeight:600px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        if(returnValue==1)
        {
            window.location.reload();
        }
    }
</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers ><asp:PostBackTrigger ControlID="btnExport" /></Triggers>
<ContentTemplate>
<div>
<table width="100%">
    <tr>
    <td  style="width:8%;" align="right">项目名称:</td>
    <td style="width:20%; height:42px" valign="top">
    <cc1:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px" Width="120px"
     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlProName_SelectedIndexChanged">
    </cc1:ComboBox>
    </td>
    <td  style="width:8%;" align="right">工程名称:</td>
    <td style="width:20%" valign="top">
        <cc1:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px" Width="120px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlEngName_SelectedIndexChanged">
        </cc1:ComboBox>
    </td>
        <td  style="width:8%;" align="right">技&nbsp;&nbsp;术&nbsp;&nbsp;员:</td>
    <td style="width:20%" valign="top">
        <cc1:ComboBox ID="ddlTecName" runat="server" AutoPostBack="true" Height="15px" Width="100px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlEngName_SelectedIndexChanged">
        </cc1:ComboBox>
    </td>
    
 
    <td rowspan="2">
        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" Text="重 置" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_OnClick" OnClientClick="return confirm('确认导出吗？')" Text="导 出" /></td>
    </tr>
    <tr>
        <td  style="width:8%;" align="right">登记日期:</td>
    <td style="width:20%" valign="top"><cc1:ComboBox ID="ddlYear" runat="server" AutoPostBack="true" Height="15px" Width="52px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlFY_OnSelectedIndexChanged"></cc1:ComboBox>
       <cc1:ComboBox ID="ddlMonth" runat="server" AutoPostBack="true" Height="15px" Width="52px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
            <asp:ListItem Text="-月份-" Value="%" ></asp:ListItem>
            <asp:ListItem Text="1月" Value="01"></asp:ListItem>
            <asp:ListItem Text="2月" Value="02"></asp:ListItem>
            <asp:ListItem Text="3月" Value="03"></asp:ListItem>
            <asp:ListItem Text="4月" Value="04"></asp:ListItem>
            <asp:ListItem Text="5月" Value="05"></asp:ListItem>
            <asp:ListItem Text="6月" Value="06"></asp:ListItem>
            <asp:ListItem Text="7月" Value="07"></asp:ListItem>
            <asp:ListItem Text="8月" Value="08"></asp:ListItem>
            <asp:ListItem Text="9月" Value="09"></asp:ListItem>
            <asp:ListItem Text="10月" Value="10"></asp:ListItem>
            <asp:ListItem Text="11月" Value="11"></asp:ListItem>
            <asp:ListItem Text="12月" Value="12"></asp:ListItem>
            </cc1:ComboBox></td>
       <td  style="width:8%;" align="right" >发&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;运:</td>
        <td style="width:20%;height:42px" valign="top">
        <cc1:ComboBox ID="ddlFY" runat="server" AutoPostBack="true" Height="15px" Width="50px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
        </cc1:ComboBox>
    </td>
    <td  style="width:8%;" align="right" >已完成确认:</td>
        <td style="width:20%" valign="top">
        <cc1:ComboBox ID="ddlFinish" runat="server" AutoPostBack="true" Height="15px" Width="50px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
            <asp:ListItem Text="N" Value="N"></asp:ListItem>
        </cc1:ComboBox>
    </td>

    </tr>
</table>
</div>

<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0">
<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息" TabIndex="0">
<ContentTemplate>

<div class="box-wrapper">
<div style="height:6px" class="box_top"></div>
<div class="box-outer">
    <asp:Panel ID="Panel1" runat="server">
      <div style="text-align:center">没有记录!</div>
    </asp:Panel>
    <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnPreRender="GridView1_OnPreRender" CellPadding="4" ForeColor="#333333" 
        DataKeyNames="TSA_ID" >
        <FixRowColumn FixRowType="Header,Pager" TableHeight="400px" TableWidth="100%" FixColumns="" />                    
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="序号"  HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="TSA_ID" ItemStyle-Wrap="false" HeaderText="生产制号" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_SHIP" ItemStyle-Wrap="false" HeaderText="船次编号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_FY" ItemStyle-Wrap="false" HeaderText="发运" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_STATUS" ItemStyle-Wrap="false" HeaderText="确认" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_ENGTYPE" ItemStyle-Wrap="false" HeaderText="类型" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_STFORCODE" ItemStyle-Wrap="false" HeaderText="代号" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PJ_NAME" ItemStyle-Wrap="false" HeaderText="项目名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_ENGNAME" ItemStyle-Wrap="false" HeaderText="工程名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ST_NAME" ItemStyle-Wrap="false" HeaderText="技术负责人" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_DEVICENO" ItemStyle-Wrap="false" HeaderText="设备名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_SHIPTIME" ItemStyle-Wrap="false" HeaderText="集港时间"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_TOTALWGHT" ItemStyle-Wrap="false" HeaderText="总重"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_SHIPWGHT" ItemStyle-Wrap="false" HeaderText="船次重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_RECVDATE" ItemStyle-Wrap="false" HeaderText="接收日期"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TSA_ADDTIME" ItemStyle-Wrap="false" HeaderText="登记日期"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField ItemStyle-Wrap="false" >
              <ItemTemplate >
                 <asp:HyperLink ID="hlTask" CssClass="link" NavigateUrl='<%#"TM_ShipTime.aspx?engid="+Eval("TSA_ID")%>' 
                          runat="server">
                         <asp:Image ID="Image1" ImageUrl="~/Assets/icons/gadgets.gif" 
                    border="0" hspace="2" align="absmiddle" runat="server" />
                          装船                               
                        </asp:HyperLink>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="隐藏列" Visible="false" >
               <ItemTemplate >
                  <asp:Label ID="lbltcid" runat="server" Text='<%#Eval("TSA_TCCLERK") %>'></asp:Label>
               </ItemTemplate> 
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"   ForeColor="#1E5C95"  />
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPageBasic" runat="server" />
</div>
</div>
</ContentTemplate>
</cc1:TabPanel>



</cc1:TabContainer>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
