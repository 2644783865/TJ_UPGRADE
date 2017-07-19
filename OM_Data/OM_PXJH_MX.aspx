<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_PXJH_MX.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXJH_MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    培训签到表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 70%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
        #rdlstSeq td
        {
            border: 0;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function cq(obj) {
            var d = $("#table1 [id *='DA_SFCQ']:checked").length;
            var z = $("#table1 [id *='DA_SFCQ']").length;
            var cql = ((d / z) * 100).toFixed(2);
            $("#<%=txt_PX_CQL.ClientID %>").val(cql);
        }

        function checknum(obj) {
            var a = /^[0-9]+([.]{1}[0-9]+){0,1}$/;
            if (!a.test(obj.value)) {
                alert("请输入整数或者小数！！！");
                obj.value = "0";
            }

            var df = 0;
            $("#table1 [id *='DA_CXRDF']").each(function() {
                if ($(this).val() > 60) {
                    df++;
                }
            })
            var z = $("#table1 [id *='DA_CXRDF']").length;
            $("#<%=txt_PX_KSHGL.ClientID %>").val(((df / z) * 100).toFixed(2));
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSave" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSave_onserverclick">保存</a> <a runat="server" id="btnBack" href="#"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add'" onclick="javascript:history.go(-1);">
                        返回</a>
            </div>
        </div>
    </div>
    <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
        <asp:Panel runat="server" ID="panJBXX">
            <table class="tab">
                <tr>
                    <td colspan="6">
                        培训详细信息
                    </td>
                </tr>
                <tr>
                     <td>
                        编号：<asp:Label runat="server" ID="lb_PX_BH"></asp:Label>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td>
                        培训名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_XMMC" onfocus="this.blur()"></asp:TextBox>
                    </td>
                    <td>
                        培训地点：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_SJDD" onfocus="this.blur()"></asp:TextBox>
                    </td>
                    <td>
                        培训课时：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_SJXS" onfocus="this.blur()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        培训讲师姓名：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_ZJR" onfocus="this.blur()"></asp:TextBox>
                    </td>
                    <td>
                        培训师工作职务：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_ZJRZW" onfocus="this.blur()"></asp:TextBox>
                    </td>
                    <td>
                        培训日期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txt_PX_SJSJ" onfocus="this.blur()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        本次培训的主要内容<br />
                        <asp:TextBox runat="server" ID="txt_PX_ZYNR" TextMode="MultiLine" Rows="8" Width="96%"></asp:TextBox>
                    </td>
                </tr>
                <%-- <tr>
                    <td colspan="6">
                        *对于以上培训内容本人已完全理解，并保证在今后的工作中遵照执行。
                    </td>
                </tr>--%>
            </table>
            <table id="table1" width="70%" align="center" cellpadding="0" cellspacing="0" border="1">
                <asp:Repeater ID="rptNR" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <th width="50px">
                                <strong>序号</strong>
                            </th>
                            <th>
                                <strong>参训人员</strong>
                            </th>
                            <th>
                                <strong>岗位</strong>
                            </th>
                            <th>
                                <strong>出勤</strong>
                            </th>
                            <th>
                                <strong>*得分</strong>
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                            </td>
                            <td>
                                <asp:TextBox ID="DA_CXR" runat="server" Text='<%# Eval("DA_CXR")%>' onfocus="this.blur()"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DA_CXRID" Value='<%# Eval("DA_CXRID")%>' />
                            </td>
                            <td>
                                <asp:TextBox ID="ST_SEQUEN" runat="server" Text='<%# Eval("ST_SEQUEN")%>' onfocus="this.blur()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="DA_SFCQ" onclick="cq(this)" Checked='<%# Eval("DA_SFCQ").ToString()=="y"?true:false %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="DA_CXRDF" runat="server" onblur="checknum(this)" Text='<%# Eval("DA_CXRDF")%>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录!<br />
                <br />
            </asp:Panel>
            <table width="70%" border="1px" cellpadding="0" cellspacing="0">
                <tr height="25px">
                    <td width="25%" align="center" style="font-size: larger;">
                        出勤率
                    </td>
                    <td width="25%">
                        <asp:TextBox runat="server" Font-Size="Medium" ID="txt_PX_CQL" onfocus="this.blur()" Width="50%"></asp:TextBox>%
                    </td>
                    <td width="25%" align="center" style="font-size: larger;">
                        考试合格率
                    </td>
                    <td width="25%">
                        <asp:TextBox runat="server" ID="txt_PX_KSHGL" Font-Size="Medium" onfocus="this.blur()" Width="50%"></asp:TextBox>%
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="font-size: larger;">
                        培训效果评价:<br />
                        <asp:TextBox runat="server" ID="txt_PX_XGPJ" TextMode="MultiLine" Rows="6" Width="96%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td align="right" style="font-size: larger;">
                        评价人:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lb_PX_PJR" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td align="right" style="font-size: larger;" >
                        评价时间：
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lb_PX_PJSJ" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
