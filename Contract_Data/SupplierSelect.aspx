<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/PopupBase.master" CodeBehind="SupplierSelect.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.SupplierSelect" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="RightContentTitlePlace">
厂商选择   
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="PrimaryContent">
<div style="border: 1px solid #000000; overflow: auto; ">
 <div class="RightContent">
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="100%">
            <tr>
            <td></td>
               <td align="right">
                  <asp:Button ID="btnConfirm" runat="server" class="button-outer" Text="确 定" onclick="btnConfirm_Click" />
                </td>
            </tr>
            </table>
            <span><strong>指定供应商（客户）</strong></span>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
             <table width="100%">
                    <tr>
                    <td>地区：
                        <asp:DropDownList ID="dopLocation" AutoPostBack="true" runat="server"  
                            onselectedindexchanged="dopLocation_SelectedIndexChanged" >
                        </asp:DropDownList>   
                        <asp:DropDownList ID="dopLocationNext" runat="server"  AutoPostBack="true"
                            onselectedindexchanged="btnQuery_Click">
                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;
                        公司类别:
                        <asp:DropDownList ID="ddl_cstype" runat="server" AutoPostBack="true" onselectedindexchanged="btnQuery_Click">
                        <asp:ListItem Text="-请选择-" Value="%" ></asp:ListItem>
                        <asp:ListItem Text="客户" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="供应商" Value="2" ></asp:ListItem>
                        <asp:ListItem Text="客户和供应商" Value="3" ></asp:ListItem>
                        <asp:ListItem Text="技术外协分包商" Value="4" ></asp:ListItem>
                        <asp:ListItem Text="生产外协分包商" Value="5" ></asp:ListItem>
                        <asp:ListItem Text="原材料销售供应商" Value="6" ></asp:ListItem>
                        <asp:ListItem Text="其它" Value="7" ></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    <strong>助记码：</strong>
                        <asp:TextBox ID="txtZJM" runat="server"></asp:TextBox>&nbsp;&nbsp;
                        <asp:Button ID="btnQuery" runat="server" class="button-outer" Text="查 询" OnClick="btnQuery_Click" />
                    </td>
                    </tr>
                </table>
        <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField Visible="False">
                 <ItemTemplate>
                   <asp:Label ID="lblID" runat="server" Text='<%# Eval("CS_CODE") %>'>&gt;</asp:Label>
                   <asp:Label ID="lblName" runat="server" Text='<%# Eval("CS_NAME") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss"/>
                    </ItemTemplate>
               <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CS_CODE" HeaderText="编号" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CS_NAME" HeaderText="公司名称" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="公司类别" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblcs_type" runat="server" Text='<%# Eval("CS_TYPE").ToString()=="1"?"客户":Eval("CS_TYPE").ToString()=="2"?"供应商":Eval("CS_TYPE").ToString()=="3"?"客户和供应商":Eval("CS_TYPE").ToString()=="4"?"技术外协分包商":"生产外协分包商"%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CS_HRCODE" HeaderText="助记码" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CS_LOCATION" HeaderText="所在地" ItemStyle-HorizontalAlign="Left" />
            </Columns>
              <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
            </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
           
        <uc1:UCPaging ID="UCPaging1" runat="server" />

        </div>
    </div>
</div>
</div>
    </asp:Content>

