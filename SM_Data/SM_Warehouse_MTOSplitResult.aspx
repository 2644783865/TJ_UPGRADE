﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_MTOSplitResult.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MTOSplitResult"
    MasterPageFile="~/Masters/BaseMaster.Master"  Title="MTO调整单拆分操作" %>

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
                            <asp:Button ID="FVerification" runat="server" Text="反核销" OnClick="FVerification_Click" Visible="false"
                                OnClientClick="return fverificationtip()" />
                            <asp:Button ID="Verification" runat="server" Text="核销" OnClick="Verification_Click" Visible="false"
                                OnClientClick="return verificationtip()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Back" runat="server" Text="返回" OnClick="Back_Click" />&nbsp;&nbsp;&nbsp;
                            <input id="Close" type="submit" runat="server" value="关闭" onclick="return closewin();" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                    <caption style="font-size: large;">
                        MTO调整单&nbsp;<asp:Label ID="LabelAction" runat="server" ForeColor="Red"></asp:Label>&nbsp;操作</caption>
                    <tr>
                        <td style="width: 25%; text-align: left;">
                        </td>
                        <td style="width: 25%; text-align: left;">
                            &nbsp;&nbsp;&nbsp;子单MTO单号：<asp:Label ID="LabelChildCode" runat="server" Font-Size="Large"></asp:Label>
                        </td>
                        <td style="width: 25%; text-align: left;">
                            &nbsp;&nbsp;&nbsp;原MTO调整单单号：<asp:Label ID="LabelParentCode" runat="server" Font-Size="Large"></asp:Label>
                        </td>
                        <td style="width: 25%; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            &nbsp;&nbsp;&nbsp;操作结果：<asp:Label ID="LabelResult" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
            <asp:Panel ID="PanelBody" runat="server" Style="margin: 0 auto 0 auto" Width="99%"
                Height="400px" ScrollBars="Auto">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    ShowFooter="True" EmptyDataText="没有相关数据！" OnRowDataBound="GridView1_RowDataBound"
                    Caption="子单MTO单据条目"  CaptionAlign="Left">
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
                         <asp:TemplateField HeaderText="到计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTCTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCTo")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelTZN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TZN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalTZNum" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelTZQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TZQuantity")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalTZQuantity" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="从计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTCFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCFrom")%>'></asp:Label>
                            </ItemTemplate>
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
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
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
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                    ShowFooter="True" EmptyDataText="没有相关数据！" OnRowDataBound="GridView2_RowDataBound"
                    Caption="原MTO调整单条目" CaptionAlign="Left">
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
                         <asp:TemplateField HeaderText="到计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTCTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCTo")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelTZN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TZN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalTZNum" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelTZQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TZQuantity")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalTZQuantity" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="从计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTCFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCFrom")%>'></asp:Label>
                            </ItemTemplate>
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
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
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
            </asp:Panel>
        </div>
    </div>
</asp:Content>
