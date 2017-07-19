<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Xie_PlanDetail.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_PlanDetail" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务号进度
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>任务号进度查看</b>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
      <table width="70%">
      <tr>
      <td style="width:10%;text-align:right">
      任务号：
      </td>
      <td style="width:10%;text-align:left">
     <asp:Label ID="lab_engid" runat="server" Text=""></asp:Label>
      </td>
      <td style="width:10%;text-align:right">
       项目名称：
        </td>
        <td style="width:10%;text-align:left">
        <asp:Label ID="lab_proj" runat="server" Text=""></asp:Label>
        </td>
        <td>
        <asp:Button ID="btn_shangcha" runat="server" Text="上查外协单据" OnClick="btn_shangcha_onclick" />
        <asp:Button ID="btn_xiacha" runat="server" Text="下查比价单" OnClick="btn_xiacha_onclick" />
        
        </td>
      </tr>
      </table>
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                          <asp:CheckBox ID="cb" runat="server" CssClass="checkBoxCss" />
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            <asp:Label ID="MS_CODE" runat="server" Text='<%#Eval("MS_CODE")%>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_NAME" HeaderText="名称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" HeaderStyle-Wrap="true"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="MS_UWGHT" HeaderText="单重" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="MS_NUM" HeaderText="外协数量" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="marzxnum" HeaderText="比价数量" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_WXTYPE" HeaderText="外协类型" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_PROCESS" HeaderText="加工工序" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MS_XHBZ" HeaderText="外协备注" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="picno" HeaderText="比价单号" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="PIC_JGNUM" HeaderText="加工单号" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="price" HeaderText="单价" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="supplierresnm" HeaderText="供应商" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                      <asp:BoundField DataField="PIC_SUPPLYTIME" HeaderText="交货期" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BJSJ" HeaderText="实际交货期" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TA_TOTALNOTE" HeaderText="结算单号" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="PIC_BJSTATUS" HeaderText="报检情况" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="结算标记" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lab_status" runat="server" Text='<%#get_js(Eval("TA_TOTALNOTE").ToString())%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging" runat="server" />
        </div>
    </div>
</asp:Content>

