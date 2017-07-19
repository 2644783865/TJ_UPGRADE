<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_CustomerAdd.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_CustomerAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    添加顾客财产
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }

        function Check() {
            var a = document.getElementById('<%=CM_INKEEP.ClientID %>');
            var b = $("#<%=CM_CMDATE.ClientID %>").datebox('getValue');
            var c = document.getElementById('<%=InorOut.ClientID%>');
            if (a.value == "" || b.value == "" || c.options[c.selectedIndex].value == "2") {
                alert("请检查填写项！");
                event.returnValue = false;
            }
        }

        function savePick() {
            var r = Save();
            $("#<%=CM_INKEEP.ClientID %>").val(r.st_name);
            $("#<%=inKeep.ClientID %>").val(r.st_id);
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <div class="box-wrapper1">
        <div class="box-outer">
            <br />
            <div style="float: right">
                <asp:Button ID="btnsubmit" runat="server" Text="提 交" OnClientClick="Check()" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />&nbsp;&nbsp;&nbsp;
            </div>
            <br />
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="顾客财产信息" TabIndex="0">
                    <HeaderTemplate>
                        顾客财产信息
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="text-align: center; margin-top: 50px; margin-bottom: 50px">
                            <h2>
                                顾客财产</h2>
                        </div>
                        <asp:Panel ID="plbasic" runat="server">
                            <table width="100%">
                                <tr align="left">
                                    <td align="right" width="15%">
                                        项目名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_PJNAME" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td align="right" width="15%">
                                        合同号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_CONTR" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td align="right" width="15%">
                                        顾客名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_COSTERM" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right">
                                        入库库管：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_INKEEP" runat="server" Width="120px" ReadOnly="true">
                                        </asp:TextBox><input type="hidden" runat="server" id="inKeep" /><img src="../Assets/images/username_bg.gif"
                                            onclick="SelPersons()" align="middle" style="cursor: pointer" />
                                    </td>
                                    <td align="right">
                                        报检日期：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_CMDATE" runat="server" Width="150px" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        项目内/外：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="InorOut" runat="server">
                                            <asp:ListItem Text="请选择" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="项目内" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="项目外" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="15%">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table width="100%">
                            <tr>
                                <td width="80%">
                                </td>
                                <td>
                                    添加设备：
                                </td>
                                <td>
                                    <input id="num" runat="server" value="1" type="text" style="width: 60px; text-align: center"
                                        onblur="CheckNum(this);" />&nbsp;&nbsp;
                                    <asp:Button ID="btnadd" runat="server" Text="增 加" OnClick="btnadd_Click" />
                                </td>
                            </tr>
                        </table>
                        <div>
                            <hr />
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table id="gr" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <asp:Repeater ID="Det_Repeater" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <td width="50px">
                                                    <strong>序号</strong>
                                                </td>
                                                <td>
                                                    <strong>任务号</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                <td>
                                                    <strong>设备名称</strong>
                                                </td>
                                                <td>
                                                    <strong>供货商名称</strong>
                                                </td>
                                                <td>
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>放置地点</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                    <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                    </asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TSA_ID" runat="server" Text='<%# Eval("TSA_ID")%>' Width="120px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_PIC" runat="server" Text='<%# Eval("CM_PIC")%>' TextMode="MultiLine"
                                                        Height="25px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_EQUIP" runat="server" Text='<%# Eval("CM_EQUIP")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_APPNAME" runat="server" Text='<%# Eval("CM_APPNAME")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_NUM" runat="server" Text='<%# Eval("CM_NUM")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_PLACE" runat="server" Text='<%# Eval("CM_PLACE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CM_NOTE" runat="server" TextMode="MultiLine" Height="25px" Text='<%#Eval("CM_NOTE")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!
                                </asp:Panel>
                                <br />
                                <div>
                                    <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" Visible="false" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="delete" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="抄送" TabIndex="0">
                    <HeaderTemplate>
                        抄送
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" EnableViewState="False">
                        </asp:Panel>
                        <asp:Panel ID="panCS" runat="server">
                            质量部：
                            <asp:CheckBoxList runat="server" ID="cbxCS" CellSpacing="10" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <asp:Panel ID="panTH" runat="server">
                            技术部：
                            <asp:CheckBoxList runat="server" ID="cbxTH" CellSpacing="10" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <asp:Panel ID="panPD" runat="server">
                            生产部：
                            <asp:CheckBoxList runat="server" ID="cbxPD" CellSpacing="10" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
</asp:Content>
