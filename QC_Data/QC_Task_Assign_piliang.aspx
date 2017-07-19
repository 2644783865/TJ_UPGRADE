<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_Task_Assign_piliang.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Task_Assign_piliang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    质量任务批量分工
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <script src="../JS/PickPersons.js" type="text/javascript"></script>
    <link href="../FM_Data/StyleFile/model.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        //点击确定
        function savePick() {
            var r = Save();
            $("#<%=txtqcclerk.ClientID %>").val(r.st_name);
            $("#<%=txtqcclerkid.ClientID %>").val(r.st_id);
            $('#win').dialog('close');
        }
    </script>

    <div class="popup_Container">
        <%--黄色--%>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <div class="TitlebarRight" onclick="cancel();">
                    </div>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <%--白色100%--%>
            <div class="box-outer">
                <div class="popup_Body">
                    <asp:Panel ID="Panel1" Width="80%" runat="server">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            width="100%">
                            <tr>
                            <td class="tdleft">任务号:</td><td class="tdright">
                            <asp:Label ID="lblTaskId" runat="server" Text=""></asp:Label>
                            </td>
                            </tr>
                          
                            <tr>
                                <td class="tdleft">
                                    质检员:
                                </td>
                                <td class="tdright">
                                    <input id="txtqcclerk" runat="server" readonly="readonly" type="text" value="" />
                                    <input id="txtqcclerkid" runat="server" readonly="readonly" type="text" value=""
                                        style="display: none" />
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" onClick="SelPersons()">
                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                            </tr>
                          
                            <tr>
                                <td class="tdleft">
                                    备注：
                                </td>
                                <td class="tdright">
                                    <asp:TextBox ID="bz" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="确  定" />
                                    <asp:Button ID="btnCancel" runat="server" Text="作废停工" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnClose" runat="server" Text="关闭" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
         
    </div>
    <div><div id="win" visible="false">
            <div>
                <table>
                    <tr>
                    <td><strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            按部门查询：
                        </td>
                        <td>
                            <input id="dep" name="dept" value="12">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 430px; height: 230px">
                <table id="dg">
                </table>
            </div>
        </div>
        <div id="buttons" style="text-align: right" visible="false">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
        </div></div>
</asp:Content>
