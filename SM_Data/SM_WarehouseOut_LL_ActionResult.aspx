<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOut_LL_ActionResult.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOut_LL_ActionResult"
    Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" language="javascript">
    
    function mergetip() {
        var retVal = confirm("确定进行合并？");
        return retVal;
    }

    function verificationtip() {
        var retVal = confirm("确定进行红字核销？");
        return retVal;
    }
     function fverificationtip() {
        var retVal = confirm("确定进行红字反核销？");
        return retVal;
    }


    /*关闭窗口*/
    function closewin() {
        window.opener.location = window.opener.location.href;
        window.close();
    } 
    
</script>


    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                        </td>
                        <td align="right">
                            <asp:Button ID="Merge" runat="server" Text="合并" OnClick="Merge_Click" OnClientClick="return mergetip()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Back" runat="server" Text="返回" OnClick="Back_Click" />&nbsp;&nbsp;&nbsp;
                            <input id="Close" type="submit" runat="server" value="关闭" onclick="return closewin();" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <asp:Panel ID="HeadPanel" runat="server" Width="100%">
            <table width="100%">
                <caption style="font-size: large;">
                    出库单&nbsp;<asp:Label ID="LabelAction" runat="server"></asp:Label>&nbsp;操作</caption>
                <tr>
                    <td style="width: 25%; text-align: left;">
                    </td>
                    <td style="width: 25%; text-align: left;">
                        &nbsp;&nbsp;&nbsp;子单/红字出库单单号：<asp:Label ID="LabelChildCode" runat="server" Font-Size="Large" style="display: none"></asp:Label>
                        <asp:Label ID="LabelTrueChildCode" runat="server" Font-Size="Large"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left;">
                        &nbsp;&nbsp;&nbsp;原出库单单号：<asp:Label ID="LabelParentCode" runat="server" Font-Size="Large" style="display: none"></asp:Label>
                        <asp:Label ID="LabelTrueParentCode" runat="server" Font-Size="Large"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">
                        &nbsp;&nbsp;&nbsp;操作结果：<asp:Label ID="LabelResult" runat="server" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="overflow: auto; width: 100%; height: 450px;">
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                ShowFooter="True" EmptyDataText="没有相关数据！" OnRowDataBound="GridView1_RowDataBound"
                Caption="子单/红字出库单据条目">
                <Columns>
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelFoot" runat="server" Text="合计："></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="型号规格" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="张(支)数" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalQN" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                <RowStyle BackColor="#EFF3FB" Wrap="false" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" Wrap="false" />
            </asp:GridView>
            <br />
            <br />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                ShowFooter="True" EmptyDataText="没有相关数据！" OnRowDataBound="GridView2_RowDataBound"
                Caption="原出库单条目">
                <Columns>
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelFoot" runat="server" Text="合计："></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="型号规格" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="张（支）数" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalQN" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
