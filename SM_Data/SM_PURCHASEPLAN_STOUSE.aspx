<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_PURCHASEPLAN_STOUSE.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_PURCHASEPLAN_STOUSE"
    Title="占有查询" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:HiddenField ID="hfdptc" runat="server" />
    
   <div style=" margin: auto; height:200px; OVERFLOW-x:auto; OVERFLOW-y:hidden; width: 950px; ">
   
   
    <table id="tab" class="nowrap fixtable fullwidth" align="center">
    
        <asp:Repeater ID="tbpc_purshaseplancheck_datialRepeater" runat="server">
            <HeaderTemplate>
                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                    <td>
                        <strong>行号</strong>
                    </td>
                    <td>
                        <strong>计划号</strong>
                    </td>
                   
                    <td>
                        <strong>物料编码</strong>
                    </td>
                    <td>
                        <strong>名称</strong>
                    </td>
                    <td>
                        <strong>规格</strong>
                    </td>
                    <td>
                        <strong>材质</strong>
                    </td>
                    <td>
                        <strong>国标</strong>
                    </td>
                    <td>
                        <strong>单位</strong>
                    </td>
                    <td>
                        <strong>计划数量</strong>
                    </td>
                    <td>
                        <strong>使用库存数量</strong>
                    </td>
                    
                    <td>
                        <strong>备注</strong>
                    </td>
                     <td>
                        <strong>项目</strong>
                    </td>
                    <td>
                        <strong>工程</strong>
                    </td>
                    <td>
                        <strong>计划人</strong>
                    </td>
                    <td>
                        <strong>计划时间</strong>
                    </td>
                    <td>
                        <strong>审核人</strong>
                    </td>
                    <td>
                        <strong>审核时间</strong>
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="baseGadget">
                    <td>
                        <%#Container.ItemIndex + 1 %>
                    </td>
                    <td>
                        <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                        &nbsp;
                    </td>
                   
                    <td>
                        <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PUR_USTNUM" runat="server" Text='<%#Eval("PUR_USTNUM")%>'></asp:Label>
                    </td>
                    <td>
                        <%#Eval("PUR_NOTE")%>
                    </td>
                     <td>
                        <%#Eval("pjnm")%>
                    </td>
                    <td>
                        <%#Eval("engnm")%>
                    </td>
                    <td>
                        <%#Eval("tjr")%>
                    </td>
                    <td>
                        <%#Eval("tjtime")%>
                    </td>
                    <td>
                        <%#Eval("shr")%>
                    </td>
                    <td>
                        <%#Eval("shtime")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="19" align="center">
                <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                    没有数据！</asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
