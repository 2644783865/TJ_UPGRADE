<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_Xie_List_Detail.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_List_Detail" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产外协管理
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
     <script src="pm_JS/SelectCondition.js" type="text/javascript"></script>
    <script src="../JS/PDMN.js" type="text/javascript" charset="GB2312"></script>
    <script type="text/javascript">
        function selectOrgAll(obj) {
            var table = document.getElementById('<%= GridView1.ClientID %>');
            if (obj.checked) {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = true;
                }
            }
            else {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = false;
                }
            }
        }
        function viewCondition() {
            document.getElementById("<%=select.ClientID%>").style.display = 'block';
        }
 </script>
<style type="text/css">
.gridviewcss
{

width:100%;background:#EEF7FD;white-space:normal;
border:1px #B3CDE8 solid;border-collapse:collapse;

}

</style>
<asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 10%; height: 24px">
                            <b>生产外协管理</b>
                        </td>
                        <td id="td_choose" runat="server" style="width:20%;text-align:left">
                            全选/取消<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
                            <asp:Button ID="btnSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_Click"
                                Text="连选" />
                        </td>
                        <td>
                        <asp:Button runat="server" ID="btnChuli" Text="处理" Width="30px" OnClick="btnChuli_OnClick" Visible="false" />
                        </td>
                        <td style="width:50%;text-align:right">
                             <asp:Button ID="btnShowPopup" runat="server" Text="筛选" 
                                 OnClientClick="viewCondition()" />&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="select"  Y="102" X="600" CancelControlID="guan">
                            </asp:ModalPopupExtender>
                        </td>
                        <td align="justify">
                            <asp:Button BackColor="Red" ID="btn_allpush" runat="server" Text="汇总" OnClick="btn_allpush_Click" Visible="false"/>
                        </td>
                    </tr>
                </table>
               <asp:Panel ID="select" Style="display: none; border-style: solid; border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                                <table width="400px;">
                                     <tr>
                                        <td>
                                           <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                    <a id="guan" onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                                        text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                                </div>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60px">
                                                    <asp:DropDownList ID="screen1" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation1" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt1" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen2" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation2" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt2" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen3" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation3" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt3" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen4" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation4" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt4" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen5" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation5" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt5" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen6" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation6" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen7" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation7" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt7" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic7" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen8" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation8" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt8" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic8" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen9" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation9" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt9" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLogic9" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60px">
                                                        <asp:DropDownList ID="screen10" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" width="100px">
                                                        <asp:DropDownList ID="ddlRelation10" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt10" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: right">
                                                        <asp:Button ID="reset" runat="server" Text="重 置" OnClick="reset_Click" />&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnfind" runat="server" Text="搜 索" OnClick="search_Click" />&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
            </div>
        </div>
    </div>
  <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" >
        <ContentTemplate>--%>
            <div>
                <table width="100%">
                    <tr>
                        <td style="width: 5%" align="right">
                            任务号:
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="tsa_id" runat="server" Width="100%" />
                        </td>
                        <td style="width: 5%" align="right">
                            合同名称:
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lab_proname" runat="server" Width="100%" />
                        </td>
                         <td style="width: 5%" align="right">
                            项目名称:
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lab_proj" runat="server" Width="100%" />
                        </td>
                        <asp:Label ID="lab_pjid" runat="server" Visible="false" />
                        <td style="width: 5%" align="right">
                            设备名称:
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lab_engname" runat="server" Width="100%" />
                        </td>
                        <td style="width: 5%" align="right">
                            批号:
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="ms_no" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8%" align="right">
                            编制人:
                        </td>
                        <td colspan="1">
                            <asp:Label ID="lbltcname" runat="server" Width="100%" />
                        </td>
                        <td style="width: 8%" align="right">
                            编制日期:
                        </td>
                        <td style="width: 14%">
                            <asp:Label ID="txt_plandate" runat="server" Width="100%" />
                        </td>
                        <td id="doc" style="width: 5%" align="right">
                            <asp:Label ID="t_doc" Text="外协单号:" runat="server" Width="100%" Visible="false" />
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="txt_docnum" runat="server" Width="100%" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_move" runat="server" OnClick="btn_move_Click" Visible="false" Text="取消外协" />
                        </td>
                        <td colspan="2" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="addscwx" CssClass="hand" runat="server">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/pcadd.gif"/>提交生产外协
                            </asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="addscwx"
                                PopupControlID="palscwx" Position="Bottom" OffsetX="-65" OffsetY="4" CacheDynamicResults="false" >
                            </asp:PopupControlExtender>
                        </td>
                    </tr>
                </table>
            </div>
               <%-- </ContentTemplate>
    </asp:UpdatePanel> --%>
          <asp:Panel ID="palscwx" Style="visibility: hidden; border-style: solid; border-width: 1px;
                border-color: blue; background-color: menu; position: absolute;" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="250px" style="height:200px">
                            <tr>
                                <td>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                        <a   onclick="document.body.click(); return false;"  style="background-color: #6699CC;
                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                            title="关闭">X</a>
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    外协类别：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dplscwx_Select" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="工序外协" Value="工序外协"></asp:ListItem>
                                        <asp:ListItem Text="成品外协" Value="成品外协"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnproplan_Click"
                                        UseSubmitBehavior="false" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="gridviewcss" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chbxcheck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="MS_TUHAO" HeaderText="图号">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_NAME" HeaderText="名称" HeaderStyle-Width="100px" >
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_GUIGE" HeaderText="规格">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                    <asp:BoundField DataField="MS_UNUM" HeaderText="单台数量" ItemStyle-HorizontalAlign="Center"  />
                                     <asp:BoundField DataField="MS_NUM" HeaderText="总数量" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="MS_TUWGHT" DataFormatString="{0:N2}" HeaderText="图纸单重(kg)">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_TUTOTALWGHT" DataFormatString="{0:N2}" HeaderText="图纸总重(kg)">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_MASHAPE" HeaderText="材料种类" HeaderStyle-Width="10px" HeaderStyle-Wrap="false" >
                                    <ItemStyle HorizontalAlign="Center" Width="20px"  Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_TECHUNIT" HeaderText="单位">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_YONGLIANG" HeaderText="材料用量">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_MATOTALWGHT" HeaderText="材料总重(kg)">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_LEN" HeaderText="长度">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_WIDTH" HeaderText="宽度">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_NOTE" HeaderText="下料备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"  Width="100px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_XIALIAO" HeaderText="下料方式">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_KU" HeaderText="库">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MS_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"  Width="100px" Wrap="true" />
                                </asp:BoundField>                       
                                    <asp:TemplateField HeaderText="主键" FooterStyle-CssClass="hidden " ItemStyle-CssClass="hidden "
                                        HeaderStyle-CssClass="hidden ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmsid" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>
                                            <asp:Label ID="lblmarid" runat="server" Text='<%#Eval("MS_MARID") %>'></asp:Label>
                                            <asp:Label ID="lblkeycoms" runat="server" Text='<%#Eval("MS_KEYCOMS") %>'></asp:Label>
                                            <asp:Label ID="lblpdstate" runat="server" Text='<%#Eval("MS_PDSTATE") %>'></asp:Label>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("MS_STATUS") %>'></asp:Label>
                                            <asp:Label ID="lblwaixie" runat="server" Text='<%#Eval("MS_scwaixie") %>'></asp:Label>
                                            <asp:Label ID="lblwxtype" runat="server" Text='<%#Eval("MS_wxtype") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" CssClass="Freezing" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0,1" />
                            </yyc:SmartGridView>
                            <div style="text-align:center">
                            <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red" >
                              <h2 >没有记录!  </h2>  </asp:Panel> </div>
                            <div>
                                <table id="tab_person" runat="server" width="100%" style="text-align: center">
                                    <tr>
                                        <td>
                                            负责人:
                                            <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            申请人:
                                            <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                        </td>  
                                        <td>
                                        每页显示：<asp:DropDownList ID="ddl_xianshi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_xianshi_change" >
                                        <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100" ></asp:ListItem>
                                        <asp:ListItem Text="200" Value="200" ></asp:ListItem>
                                        <asp:ListItem Text="400" Value="400"></asp:ListItem>
                                        <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                        <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                        <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
                                        <asp:ListItem Text="5000" Value="5000"></asp:ListItem>
                                        <asp:ListItem Text="10000" Value="10000"></asp:ListItem>
                                        </asp:DropDownList>条
                                        
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hfpldetail" runat="server" />
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            </div>
          </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>