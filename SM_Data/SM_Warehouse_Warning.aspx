<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_Warning.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Warning"
    Title="安全库存" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 200px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

    <script language="javascript" type="text/javascript">


        function allsel() {
            var table = document.getElementById('<%= GridView1.ClientID %>');
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                }
            }
        }

        function cancelsel() {
            var table = document.getElementById('<%= GridView1.ClientID %>');
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                }
            }
        }

        function consel() {
            table = document.getElementById('<%= GridView1.ClientID %>');
            tr = table.getElementsByTagName("tr"); //这里的tr是一个数组
            for (var i = 1; i < (tr.length - 1); i++) {
                obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (obj.type.toLowerCase() == "checkbox" && obj.value != "") {
                    if (obj.checked) {
                        obj.checked = true;

                        for (var j = i + 1; j < tr.length; j++) {
                            var nextobj = tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                            if (nextobj != null) {

                                if (nextobj.type.toLowerCase() == "checkbox" && nextobj.value != "") {
                                    if (nextobj.checked) {
                                        for (var k = i + 1; k < j + 1; k++) {
                                            tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title">
                <div align="left" style="display: inline">
                    &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                        id="continue" type="button" value="连选" onclick="consel()" />
                    &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
                </div>
                <div align="left" style="display: inline">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;类别：<asp:DropDownList runat="server" ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_Search">
                        <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                        <asp:ListItem Text="钢材类" Value="1"></asp:ListItem>
                        <asp:ListItem Text="焊材类" Value="2"></asp:ListItem>
                        <asp:ListItem Text="耗材类" Value="3"></asp:ListItem>
                         <asp:ListItem Text="油漆类" Value="4"></asp:ListItem>
                          <asp:ListItem Text="采购成品类" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div align="right" style="display: inline; float: right">
                    <asp:HyperLink ID="addpcpurbill" CssClass="hand" runat="server">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />
                        新增采购申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="addpcpurbill"
                        PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-250" CacheDynamicResults="false">
                    </asp:PopupControlExtender>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="palPSHTLB" Style="visibility: hidden; border-style: solid; border-width: 1px;
        border-color: blue; background-color: Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="350px">
                    <tr>
                        <td>
                            <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                    cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                    title="关闭">X</a>
                            </div>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            任务号:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_pjinfo" runat="server" Text="" OnTextChanged="tb_pjinfo_Textchanged"
                                AutoPostBack="True"></asp:TextBox>
                            <asp:TextBox ID="tb_pj" runat="server" Visible="false" Text=""></asp:TextBox>
                            <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_pjinfo"
                                ServicePath="/PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetTask" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </cc1:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            项目名称:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_enginfo" runat="server" Text="" Width="150px"></asp:TextBox>
                            <asp:TextBox ID="tb_htid" runat="server" Text="" Width="150px" Visible="false"></asp:TextBox>
                            <%--<asp:TextBox ID="tb_enginfo" runat="server"  Text="" Width="150px"></asp:TextBox>
                             <asp:TextBox ID="tb_htid" runat="server"  Text="" Width="150px" Visible="false"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            物料类型:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                <asp:ListItem Text="定尺板" Value="0"></asp:ListItem>
                                <asp:ListItem Text="非定尺板" Value="1"></asp:ListItem>
                                <asp:ListItem Text="型材" Value="2"></asp:ListItem>
                                <asp:ListItem Text="釆" Value="3"></asp:ListItem>
                                <asp:ListItem Text="锻" Value="4"></asp:ListItem>
                                <asp:ListItem Text="铸" Value="5"></asp:ListItem>
                                <asp:ListItem Text="采购成品" Value="6"></asp:ListItem>
                                <asp:ListItem Text="非" Value="7"></asp:ListItem>
                                <%--<asp:ListItem Text="电气电料" Value="8"></asp:ListItem>--%>
                                <asp:ListItem Text="油漆" Value="9"></asp:ListItem>
                                <asp:ListItem Text="其他" Value="10"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            <%-- <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click" />--%>
                            <asp:Button ID="btnConfirm" runat="server" Text="确定" OnClick="btnConfirm_Click" UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"  OnRowDataBound="GridView1_OnRowDataBound"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="物料编码" DataField="MARID"></asp:BoundField>
                    <asp:BoundField HeaderText="物料名称" DataField="MNAME"></asp:BoundField>
                    <asp:BoundField HeaderText="规格型号" DataField="GUIGE"></asp:BoundField>
                    <asp:BoundField HeaderText="材质" DataField="CAIZHI"></asp:BoundField>
                    <asp:BoundField HeaderText="国标" DataField="GB"></asp:BoundField>
                    <asp:BoundField HeaderText="合理库存量" DataField="REASONABLENUM"></asp:BoundField>
                    <asp:BoundField HeaderText="报警数量" DataField="WARNNUM"></asp:BoundField>
                     <asp:BoundField HeaderText="单位" DataField="PURCUNIT"></asp:BoundField>
                    <asp:TemplateField HeaderText="类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <label runat="server" id="lblType">
                                <%# Eval("Type").ToString() == "1" ? "钢材类" : Eval("Type").ToString() == "2" ? "焊材类" : Eval("Type").ToString() == "3" ? "耗材类" : Eval("Type").ToString() == "4" ? "油漆类" :Eval("Type").ToString() == "5" ? "采购成品类" : ""%></label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="即时库存" DataField="STORAGENUM"></asp:BoundField>
                    <asp:BoundField HeaderText="采购数量" DataField="NUM" Visible="false"></asp:BoundField>
                    <asp:TemplateField HeaderText="是否班组结算物料" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                     <label runat="server" id="lblzjsbz"><%# Eval("BZJSBZ").ToString() == "1" ? "是" : "否"%></label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="制单人" DataField="TJRNM" Visible="false"></asp:BoundField>
                    <asp:BoundField HeaderText="制单时间" DataField="TJDATE" Visible="false"></asp:BoundField>
                    <asp:TemplateField HeaderText="下推状态" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="MP_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("TOTALSTATE").ToString())%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审核状态" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="MP_SPZTTEXT" runat="server" Text='<%#get_spzt(Eval("MP_SPZT").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
