<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_TurnForLibrary.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_TurnForLibrary" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划表      
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
 <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
 <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="box-inner">
   <div class="box_right">
    <div class="box-title">
        <table width="96%">
        <tr>
            <td style="width:21%">生产制号：<asp:Label ID="tsaid" runat="server"></asp:Label></td>
            <td style="width:21%">项目名称：<asp:Label ID="proname" runat="server"></asp:Label></td>
            <td style="width:21%">工程名称：<asp:Label ID="engname" runat="server"></asp:Label></td>
            <td align="right">
                <asp:Button ID="btnForLib" runat="server" Text="转备库" 
                    onclick="btnForLib_Click" /></td>
         </tr>
        </table>
       </div>
     </div>
</div>

    <div class="box-wrapper">
        <div class="box-outer">
            <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                    runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                        <ItemTemplate>
                            <asp:Label ID="Index" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NAME" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_SUBMITNAME" HeaderText="采购员" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_SUBMITTM" HeaderText="提交时间" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有需要备库的材料!</asp:Panel>
        </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
