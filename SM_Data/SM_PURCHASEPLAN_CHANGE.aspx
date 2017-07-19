<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_PURCHASEPLAN_CHANGE.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_PURCHASEPLAN_CHANGE"
    Title="物料变更信息" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

  <asp:HiddenField ID="hfdptc" runat="server" />
  
  
      <div style=" margin: auto; height:200px; OVERFLOW-x:auto; OVERFLOW-y:hidden; width: 935px; ">
       
        <table id="tab" class="nowrap fixtable fullwidth" align="center">
            <asp:Repeater ID="tbpc_purbgclallRepeater" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                        <td>
                            <strong>行号</strong>
                        </td>
                        
                        <td>
                            <strong>计划跟踪号</strong>
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
                            <strong>长度</strong>
                        </td>
                        <td>
                            <strong>宽度</strong>
                        </td>
                        <td>
                            <strong>单位</strong>
                        </td>
                        <td>
                            <strong>原计划数量</strong>
                        </td>
                        <td>
                            <strong>变更数量</strong>
                        </td>
                        <td>
                            <strong>辅助单位</strong>
                        </td>
                        <td>
                            <strong>变更辅助数量</strong>
                        </td>
                        <td>
                            <strong>执行数量</strong>
                        </td>
                        <td>
                            <strong>执行辅助数量</strong>
                        </td>
                        <td>
                            <strong>是否执行</strong>
                        </td>
                        <td>
                            <strong>执行人</strong>
                        </td>
                        <td>
                            <strong>批号</strong>
                        </td>
                        <td>
                            <strong>项目名称</strong>
                        </td>
                        <td>
                            <strong>工程名称</strong>
                        </td>
                        <td>
                            <strong>技术员</strong>
                        </td>
                        <td>
                            <strong>计划日期</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                        <td>
                            <%#Container.ItemIndex + 1 %>
                        </td>
                       
                        <td>
                            <asp:Label ID="BG_PTCODE" runat="server" Text='<%#Eval("BG_PTCODE")%>'></asp:Label>
                        </td>
                       
                        <td>
                            <%#Eval("BG_MARID")%>
                        </td>
                        <td>
                            <%#Eval("BG_MARNAME")%>
                        </td>
                        <td>
                            <%#Eval("BG_MARNORM")%>
                        </td>
                        <td>
                            <%#Eval("BG_MARTERIAL")%>
                        </td>
                        <td>
                            <%#Eval("BG_GUOBIAO")%>
                        </td>
                        <td>
                            <%#Eval("LENGTH")%>
                        </td>
                        <td>
                            <%#Eval("WIDTH")%>
                        </td>
                        <td>
                            <%#Eval("BG_NUNIT")%>
                        </td>
                        <td>
                        <asp:Label runat="server" ID="LabelPrenum"></asp:Label>
                        </td>
                        <td>
                            <%#Eval("BG_NUM")%>
                        </td>
                        <td>
                            <%#Eval("fzunit")%>
                        </td>
                        <td>
                            <%#Eval("BG_FZNUM")%>
                        </td>
                        <td>
                            <%#Eval("BG_ZXNUM")%>
                        </td>
                        <td>
                            <%#Eval("BG_ZXFZNUM")%>
                        </td>
                        <td>
                            <asp:Label ID="BG_STATETEXT" runat="server" Text='<%#Eval("BG_STATE").ToString()=="0"?"否":"是"%>'></asp:Label>
                        </td>
                        <td>
                            <%#Eval("BG_ZXRENNM")%>
                        </td>
                         <td>
                            <%#Eval("MP_PJNAME")%>
                        </td>
                        <td>
                            <%#Eval("MP_ENGNAME")%>
                        </td>
                         <td>
                            <%#Eval("MP_NEWPID")%>
                        </td>
                        <td>
                            <%#Eval("jsy")%>
                        </td>
                        <td>
                            <%#Eval("jhtime")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="22" align="center">
                    <asp:Panel ID="NoDataPanebg" runat="server" Visible="false">
                        没有数据！</asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
