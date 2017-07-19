<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_MpSummary.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MpSummary" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="RightContentTitlePlace">原始数据勾选项/查询项汇总
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
         <table width="98%">
          <tr>
            <td>
                排序方式:<asp:DropDownList ID="ddlSort" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="序号" Value="dbo.f_FormatSTR(BM_XUHAO,'.')"></asp:ListItem>
                   <asp:ListItem Text="总序" Value="dbo.f_FormatSTR(BM_ZONGXU,'.')" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="图号" Value="BM_TUHAO" ></asp:ListItem>
                   <asp:ListItem Text="材料名称" Value="BM_MANAME"></asp:ListItem>
                   <asp:ListItem Text="材料规格" Value="BM_MAGUIGE"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSortOrder" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="升序" Value="asc" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="降序" Value="desc"></asp:ListItem>
            </asp:DropDownList></td>
          </tr>
         </table>
       </div>
      </div>
    </div>
    <div class="box-wrapper">
       <div class="box-outer">  
       <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large">没有记录!</asp:Panel>
    <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" ShowFooter="true"
        onrowdatabound="GridView1_RowDataBound" AllowPaging="False">
        <RowStyle BackColor="#EFF3FB"  Wrap="false" />
        <HeaderStyle Wrap="false" />
        <Columns>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MSXUHAO"  HeaderText="明细序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" HeaderStyle-Wrap="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"   HeaderText="物料编码"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"   HeaderText="总序"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"   HeaderText="中文名称"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ENGSHNAME"   HeaderText="英文名称" Visible="false" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE"   HeaderText="规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"   HeaderText="库"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
           
            <asp:BoundField DataField="BM_KEYCOMS"   HeaderText="关键部件"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MANAME"   HeaderText="材料名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:F2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:F2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:F2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MPMY"   DataFormatString="{0:F2}" HeaderText="计划面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_SINGNUMBER"   HeaderText="单台数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NUMBER"   HeaderText="总数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PNUMBER"   HeaderText="计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT"   DataFormatString="{0:F2}" HeaderText="单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT"   DataFormatString="{0:F2}" HeaderText="总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE"   HeaderText="材料种类"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
          
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FooterStyle BackColor="#B9D3EE" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px"  TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
        </div>
    </div>     
            
</asp:Content>
