<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_ServiceConn.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceConn"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务联系单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function Check() {
            var a = '<%=sess %>';
            if (a == "0201") {
                var a = document.getElementById('<%=CM_CLPART.ClientID%>').getElementsByTagName('input');
                var b = false;
                for (var i = 0; i < a.length; i++) {
                    if (a[i].checked) {
                        b = true;
                    }
                }
                if (!b) {
                    alert("请选择部门！");
                    return false;
                }
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="确 定" OnClientClick="return Check();" />
        <asp:Button ID="btnShenP" runat="server" OnClick="btntijiao_Click" Text="提交审批" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" OnClick="btnreturn_Click" Text="返 回" />
        &nbsp;&nbsp;&nbsp;
    </div>
    <br />
    <asp:TabContainer ID="TabContainer1" runat="server" Width="99%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <asp:TabPanel ID="tb1" runat="server" TabIndex="0" HeaderText="顾客服务联系单">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center; width: 99%">
                        <div style="text-align: center; margin-top: 25px">
                            <h2>
                                顾客服务联系单</h2>
                        </div>
                        <table width="800px" style="margin: auto">
                            <tr>
                                <td style="text-align: right; font-size: medium">
                                    文件号：TJZJ-R-M-17
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; font-size: medium">
                                    编号：<asp:Label runat="server" ID="lb_bh"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panel" runat="server">
                            <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto">
                                <tr>
                                    <td width="100px">
                                        项目名称：
                                    </td>
                                    <td>
                                        <asp:Label ID="CM_PJNAME" runat="server" Width="120px"></asp:Label>
                                    </td>
                                    <td width="100px">
                                        合同号：
                                    </td>
                                    <td>
                                        <asp:Label ID="CM_CONTR" runat="server" Width="120px"></asp:Label>
                                    </td>
                                    <td width="100px">
                                        设备名称：
                                    </td>
                                    <td>
                                        <asp:Label ID="CM_EQUIP" runat="server" Width="120px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        顾客名称：
                                    </td>
                                    <td>
                                        <asp:Label ID="CM_CUSNAME" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        信息简介：
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="CM_INFO" runat="server" Width="240px"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        至部门
                                    </td>
                                    <td colspan="5">
                                        <p style="text-align: left; padding-left: 80px">
                                            <asp:CheckBoxList ID="CM_CLPART" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                CellPadding="5">
                                            </asp:CheckBoxList>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        要求服务内容：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_COMMAND" runat="server" Height="100px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                        <asp:FileUpload ID="FileUp" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                            Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                                        <br />
                                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                        <div align="center">
                                            <asp:GridView ID="GridView" runat="server" CellPadding="4" CssClass="toptable grid"
                                                AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                                Width="80%">
                                                <Columns>
                                                    <asp:BoundField DataField="ShowName" HeaderText="文件名称">
                                                        <ControlStyle Font-Size="Small" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间">
                                                        <ControlStyle Font-Size="Small" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                                Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                                                ToolTip="删除" />
                                                        </ItemTemplate>
                                                        <ControlStyle Font-Size="Small" />
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="下载">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                                OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                                ToolTip="下载" />
                                                        </ItemTemplate>
                                                        <ControlStyle Font-Size="Small" />
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                                                    Height="10px" />
                                                <RowStyle BackColor="#EFF3FB" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        收货单位：
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="CM_DANWEI" runat="server" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        单位地址：
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="CM_DIZHI" runat="server" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        服务联系人：
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="CM_CONN" runat="server" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        现场联系人：
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="CM_LIVE" runat="server" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        时间要求：
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="CM_ONTIME" runat="server" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <p>
                                            注： 1进行现场服务时请填写《顾客服务信息反馈表》，并返回至市场部。</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        发文部门
                                    </td>
                                    <td colspan="5">
                                        市场部
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        经办人
                                    </td>
                                    <td>
                                        <asp:Label ID="ST_NAME" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        部门主管
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CM_DIRECTOR"></asp:Label>
                                    </td>
                                    <td>
                                        主管领导
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CM_LEADER"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tb2" runat="server" HeaderText="审批" TabIndex="1" Width="100%">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                            <asp:Panel ID="bmPanel" runat="server" Enabled="false">
                                <tr>
                                    <td width="100px">
                                        部门主管：
                                    </td>
                                    <td style="width: 524px">
                                        <asp:TextBox ID="bmspnr" runat="server" Width="350px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="bmsp" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="bmsp_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="驳回" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                        部门主管：<asp:Label runat="server" ID="bmzg"></asp:Label><input id="bmzgID" type="hidden"
                                            runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 审批日期:&nbsp;&nbsp;<asp:Label ID="bmsprq"
                                                runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="ldPanel" runat="server" Enabled="false">
                                <tr>
                                    <td width="100px">
                                        主管领导：
                                    </td>
                                    <td style="width: 524px">
                                        <asp:TextBox ID="ldspnr" runat="server" Width="350px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="ldsp" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="ldsp_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="驳回" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                        主管领导：<asp:Label runat="server" ID="zgld"></asp:Label><input id="zgldID" type="hidden"
                                            runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 审批日期:&nbsp;&nbsp;<asp:Label ID="lzsprq"
                                                runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
