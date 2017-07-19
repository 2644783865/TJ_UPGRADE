<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" EnableEventValidation="false" 
 AutoEventWireup="true" CodeBehind="TM_ShipTimeDetail.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_ShipTimeDetail" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    原始数据</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server"><asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
 <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">

<div class="box-inner" style="vertical-align:top">
            <div class="box_right">
                <div class="box-title">
                    <table width="98%">
                        <tr>
                        <td style="width:16%;" align="left">
                            BOM原始数据
                        </td>
                        <td align="right">显示级数:</td>
                        <td align="left"><asp:DropDownList ID="ddlOrgJishu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgjishu_SelectedIndexChanged">
           <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
             <asp:ListItem Text="1级" Value="0"></asp:ListItem>
             <asp:ListItem Text="2级" Value="1"></asp:ListItem>
             <asp:ListItem Text="3级" Value="2"></asp:ListItem>
             <asp:ListItem Text="4级" Value="3"></asp:ListItem>
             <asp:ListItem Text="5级" Value="4"></asp:ListItem>
             <asp:ListItem Text="6级" Value="5"></asp:ListItem>
             <asp:ListItem Text="7级" Value="6"></asp:ListItem>
             <asp:ListItem Text="8级" Value="7"></asp:ListItem>
           </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlShowType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgjishu_SelectedIndexChanged">
            <asp:ListItem Text="按总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
            <asp:ListItem Text="按序号" Value="BM_XUHAO"></asp:ListItem>
                </asp:DropDownList></td>
                
                <td align="right">
                    <asp:DropDownList ID="ddlQuery" runat="server">
                      <asp:ListItem Text="-请选择-" Value="-请选择-" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="名称" Value="BM_CHANAME"></asp:ListItem>
                      <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
                      <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" OnClick="ddlOrgjishu_SelectedIndexChanged" Text="查询" />
                    </td>
                         <td  align="center">
                         <asp:Button ID="Button1" runat="server" Text="确定" OnClick="Button1_Click"/>
                         </td>
                        </tr>
                        </table>
                 </div>
             </div>
</div>

<div class="box-wrapper">
        <div class="box-outer">
        <asp:Panel ID="NoDataPanel" Width="100%"  runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel> 
         <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
        <RowStyle BackColor="#EFF3FB" />
        
        <Columns>
        <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
        <ItemTemplate>
        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        <asp:CheckBox ID="CheckBox1" runat="server"  CssClass="checkBoxCss" />
        </ItemTemplate>
        </asp:TemplateField>
         <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"/>
         <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
         <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
         <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
        <asp:TemplateField HeaderText="中文名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
        <ItemTemplate>
        <asp:Label ID="lbchaname" runat="server" Text='<%# Eval("BM_CHANAME")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
        <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
        <asp:BoundField DataField="BM_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
        <asp:BoundField DataField="BM_UNITWGHT" HeaderText="单重" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false"/>
        <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
        <ItemTemplate>
        <asp:Label ID="lbtotalwght" runat="server" Text='<%# Eval("BM_TOTALWGHT")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
         <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle Wrap="false" BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
             <FixRowColumn FixRowType="Header,Pager" TableHeight="330px" TableWidth="100%" FixColumns="0" />  
          </yyc:SmartGridView>
          <uc1:UCPaging ID="UCPaging1" Visible="false" runat="server" />        
        </div>
</div>   
</asp:Content>
