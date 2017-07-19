<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" 
CodeBehind="FM_SaleCostSta.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_SaleCostSta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script src= "FM_JS/SelectCondition.js" type="text/javascript"></script>


<script type="text/javascript" language="javascript">
   function viewCondition()
    {
     document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
    }
    function setFlag()
    {
        document.getElementById("<%=CurType.ClientID%>").value="yes";
    }
   function verifyExport()
   {
     var extype = document.getElementById("<%=CurType.ClientID%>").value;     
     if(extype!="yes")
     {
        alert("请选择筛选条件查看");
        return false;
     }
     else
     {
        return true;
     }
   } 

 function show(id,starttime,endTime)
 {
   var sRet=window.showModalDialog("SCZHLL_PJ.aspx?id="+escape(id)+"&starttime="+starttime+"&endtime="+endTime+"&m=0","obj","dialogWidth=1200px;dialogHeight=600px;");

 }
</script>


<%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>

<div  class="box-inner">
<div class="box_right">
<div class="box-title">
<table width="98%">
<tr><td align="left">销售出库统计</td><td align="right">
  <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" 
                    TargetControlID="btnShowPopup" PopupControlID="UpdatePanelCondition" Drag="false"
                    Enabled="True"  DynamicServicePath=""   Y="30">
        </asp:ModalPopupExtender> 
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_export" runat="server" Text="导 出" OnClientClick="return verifyExport()"  OnClick="btn_export_Click"/></td>
</tr>
</table>
<asp:Panel ID="PanelCondition" runat="server" Width="700px" style="display:none"> 
     <asp:UpdatePanel ID="UpdatePanelCondition"  runat="server" UpdateMode="Conditional">
     <ContentTemplate>
     <table width="700px" style="background-color:#CCCCFF; border:solid 1px black;">
     <tr><td width="60px">物料级别：</td>
     <td align="left" height="23px">
        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
            <asp:ListItem Text="一级" Value="01"></asp:ListItem>
            <asp:ListItem Text="二级" Value="02"></asp:ListItem>
            <asp:ListItem Text="三级" Value="03" Selected="True"></asp:ListItem>
        </asp:DropDownList>
     </td>
     <td align="left" >年/月：</td>
     <td align="left" height="23px">
               <asp:RadioButtonList ID="rbl1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
               <asp:ListItem Value="month" Selected="True">月</asp:ListItem>
               <asp:ListItem Value="year">年</asp:ListItem>
               </asp:RadioButtonList>
           </td> 
      <td align="right" height="23px">
         从年月<asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
           <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
            TargetControlID="txtStartTime">
            </asp:CalendarExtender>
      </td>
      <td align="center">
      到年月<asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
      <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
      TargetControlID="txtEndTime">
      </asp:CalendarExtender> 
      </td>
     </tr>
     
     <tr>
     <td colspan="6">
         <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                        CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" OnDataBound="GridViewSearch_DataBound" >
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <Columns>
                
                <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)"  Width="128px"></asp:TextBox>
                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px"  Style="display: none">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Width="130px"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="比较关系">
                    <ItemTemplate>
                       <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)"  Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                        <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                        <asp:ListItem Value="1">等于</asp:ListItem>
                        <asp:ListItem Value="2">不等于</asp:ListItem>
                        <asp:ListItem Value="3">大于</asp:ListItem>
                        <asp:ListItem Value="4">大于或等于</asp:ListItem>
                        <asp:ListItem Value="5">小于</asp:ListItem>
                        <asp:ListItem Value="6">小于或等于</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数值">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                    <asp:DropDownList ID="ddlTypeValue" runat="server" Width="128px"  Style="display: none">
                        </asp:DropDownList>
                 </ItemTemplate>
                 <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="逻辑">
                    <ItemTemplate>
                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)"  Width="60px"></asp:TextBox>
                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                        <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                        <asp:ListItem Value="OR" Selected="True" >或者</asp:ListItem>
                        <asp:ListItem Value="AND">并且</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"/>
                </asp:TemplateField>
            </Columns>
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />  
        </asp:GridView>
     </td>
     </tr>
   
      <tr >
       <td colspan="6" align="center" height="23px">
              <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " OnClientClick="setFlag()" OnClick="btnQuery_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnClose" runat="server"  Text="取 消" OnClick="btnClose_Click"/>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnReset" runat="server" Text="重置"  OnClick="btnReset_Click"/>
           </td>
      </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
 </asp:Panel>  
</div>
</div>
</div> 
  
 <div class="box-wrapper">
        <div class="box-outer">        
    <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
   
    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
             CellPadding="4" ForeColor="#333333" Width="100%" OnRowDataBound="GridView1_RowDataBound"  >
        <RowStyle BackColor="#EFF3FB"/>
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="PTC" ControlStyle-CssClass="notbrk" HeaderText="生产制号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="SLYEAR" ControlStyle-CssClass="notbrk" HeaderText="年" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="SLMONTH" ControlStyle-CssClass="notbrk" HeaderText="月" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="MTID" ControlStyle-CssClass="notbrk" HeaderText="物料(类别)编码" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="MTNAME" ControlStyle-CssClass="notbrk" HeaderText="物料(类别)名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="Company" ControlStyle-CssClass="notbrk" HeaderText="收货方名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" ControlStyle-CssClass="notbrk" HeaderText="销售数量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="AMOUNT" ControlStyle-CssClass="notbrk" HeaderText="金额" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="100%" TableWidth="100%" FixColumns="0" />
    </yyc:SmartGridView>
   
<br />
 <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red" >
     没有记录!</asp:Panel>
     <uc1:UCPaging ID="UCPaging1" runat="server" />
     
     
     </ContentTemplate>
     </asp:UpdatePanel>
     <div>        
        <asp:TextBox ID="CurType" runat="server" Text="" style="display:none"></asp:TextBox>      
     <div>
</div>
     </div>


   
</asp:Content>
