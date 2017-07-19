<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_CustomerEdit.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_CustomerEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    修改顾客信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Check() {
            var a = document.getElementById('<%=CM_PJNAME.ClientID %>');
            var b = document.getElementById('<%=CM_CONTR.ClientID %>');
            var c = document.getElementById('<%=CM_COSTERM.ClientID %>');
            var d = document.getElementById('<%=CM_KEEPER.ClientID %>');
            var e = document.getElementById('<%=CM_CMDATE1.ClientID %>');
            if (a.value == "" || b.value == "" || c.value == "" || d.value == "" || e.value == "") {
                alert("请检查填写项！");
                event.returnValue = false;
            }
        }

        function savePick() {
            var r = Save();
            $("#<%=CM_KEEPER.ClientID %>").val(r.st_name);
            $("#<%=inKeep.ClientID %>").val(r.st_id);
            $('#win').dialog('close');
        }
    </script>

    <asp:HiddenField runat="server" ID="UserID" />
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="Panel1" runat="server">
                <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
                    text-align: left;" class="toptable grid" border="1">
                    <tr>
                        <td style="text-align: right; width: 100px">
                            项目名称：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PJNAME" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px">
                            合同号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_CONTR" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px">
                            客户组别：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_COSTERM" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            图 号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PIC" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            名 称：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_EQUIP" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            供应商名称
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_APPNAME" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            市场部日期：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_CMDATE1" Width="120px" class="easyui-datebox"
                                editable="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            数 量：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_NUM" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            放置地点：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PLACE" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            库 管：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_KEEPER" Width="100px"></asp:TextBox><input type="hidden"
                                runat="server" id="inKeep" /><img src="../Assets/images/username_bg.gif" onclick="SelPersons()"
                                    align="middle" style="cursor: pointer" />
                        </td>
                        <td style="text-align: right;">
                            项目内/外：
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="CM_INOROUT">
                                <asp:ListItem Text="项目内" Value="0"></asp:ListItem>
                                <asp:ListItem Text="项目外" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            入库日期：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_INDATE1" Width="120px" onclick="setday(this)"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            维护人：
                        </td>
                        <td>
                            <asp:Label ID="ST_NAME" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            编号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_BIANHAO" Width="120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            任务号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TSA_ID" Width="120px"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hd_tsa" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 100px">
                            备 注：
                        </td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="CM_NOTE" TextMode="MultiLine" Width="200px" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="text-align: center">
                <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" value="关 闭" onclick="window.close()" />
            </div>
        </div>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="05">
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
    </div>
</asp:Content>
