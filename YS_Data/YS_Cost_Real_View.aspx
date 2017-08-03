<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="YS_Cost_Real_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Real_View" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    实际发生费
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script type="text/javascript" language="javascript">
        var selectedColor = "#C0FF3E";
        var rowOverColor = "blue";
        var rowColor = "#EFF3FB";
        var selectedRows = new Object();

        function SelectRow(uniqueId, element) {
            if (typeof (selectedRows[uniqueId]) == "undefined")
                selectedRows[uniqueId] = false;
            selectedRows[uniqueId] = !selectedRows[uniqueId];
            element.style.backgroundColor = selectedRows[uniqueId] ? selectedColor : rowColor;
        }
      function ShowContract(id)
      {
         var autonum=Math.round(10000*Math.random()); 
         window.open("../Contract_Data/CM_Contract_SW_Add.aspx?Action=View&autonum="+autonum+"&condetail_id="+id); 
      }


      function PurMarView(CONTRACTNO, FatherCode) {
    var autonum = Math.round(10000 * Math.random());

    window.open("YS_Cost_Real_View_Detail.aspx?CONTRACTNO=" + CONTRACTNO + "&autonum=" + autonum + "&FatherCode=" + FatherCode + "");

}
       
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            合同号:
                            <asp:TextBox ID="txt_search" runat="server" Text="" Width="150px"></asp:TextBox><asp:Button
                                ID="btn_search" runat="server" Text="查询" OnClick="btn_search_OnClick" />
                        </td>
                        <td align="right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    上次更新时间：<asp:Label ID="lab_updatetime" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Btn_update" runat="server" Text="实际费用更新" OnClick="Btn_update_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:RadioButtonList ID="rbl_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="未结算" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已结算" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="ckb_JS_OK" runat="server" AutoPostBack="true" Text="只显示可结算" OnCheckedChanged="ckb_JS_OK_OnCheckedChanged" />
                        </td>
                        <td align="center">
                            项目名称:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_project_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 工程名称:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_OnClick">
                                <asp:Image ID="ModImahe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />费用结算</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_onrowdatabound">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" HeaderText="序号"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_CONTRACT_NO") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="合同号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_TSA_ID" runat="server" Text='<%#Eval("YS_TSA_ID") %>'
                                    ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("PCON_PJNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="工程名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_engname" runat="server" Text='<%#Eval("PCON_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="YS_OUT_LAB_MAR" ItemStyle-HorizontalAlign="Center" HeaderText="技术外协"
                            HeaderStyle-ForeColor="Black" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />--%>
                        <asp:BoundField DataField="YS_FERROUS_METAL" ItemStyle-HorizontalAlign="Center" HeaderText="黑色金属"
                            HeaderStyle-ForeColor="Brown" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_PURCHASE_PART" ItemStyle-HorizontalAlign="Center" HeaderText="外购件"
                            HeaderStyle-ForeColor="Brown" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_MACHINING_PART" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-ForeColor="Brown" HeaderText="加工件" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_PAINT_COATING" ItemStyle-HorizontalAlign="Center" HeaderText="油漆涂料"
                            HeaderStyle-ForeColor="Brown" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_ELECTRICAL" ItemStyle-HorizontalAlign="Center" HeaderText="电气电料"
                            HeaderStyle-ForeColor="Brown" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_OTHERMAT_COST" ItemStyle-HorizontalAlign="Center" HeaderText="其它材料费"
                            HeaderStyle-ForeColor="Brown" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_TEAM_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="直接人工费"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_FAC_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="厂内分包"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_PRODUCT_OUT" ItemStyle-HorizontalAlign="Center" HeaderText="生产外协"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--<asp:BoundField DataField="YS_MANU_COST" ItemStyle-HorizontalAlign="Center" HeaderText="制造费用"
                            HeaderStyle-ForeColor="Green" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_SELL_COST" ItemStyle-HorizontalAlign="Center" HeaderText="销售费用"
                            HeaderStyle-ForeColor="Green" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_MANAGE_COST" ItemStyle-HorizontalAlign="Center" HeaderText="管理费用"
                            HeaderStyle-ForeColor="Green" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_Taxes_Cost" ItemStyle-HorizontalAlign="Center" HeaderText="税金及附加"
                            HeaderStyle-ForeColor="Green" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />--%>
                        <asp:BoundField DataField="YS_TRANS_COST" ItemStyle-HorizontalAlign="Center" HeaderText="运费"
                            HeaderStyle-ForeColor="Green" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_MAR_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="材料费小计"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--材料费小计--%>
                       <%-- <asp:BoundField DataField="YS_FINA_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="分摊费小计"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />--%>
                        <%--财务费小计--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="是否结算" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_JSstate" runat="server" Text='<%# GetJSState(Eval("YS_ADDDATE").ToString(),Eval("YS_XS_Finished").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="是否完结" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_Finstate" runat="server" Text='<%# GetFinState(Eval("YS_XS_Finished").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
