<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ServiceAdd.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务通知单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <input type="hidden" id="lxing" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" OnClientClick="Check()" OnClick="btnsubmit_Click"
            Text="提 交" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" OnClick="btnreturn_Click" Text="返 回" />
        &nbsp;&nbsp;&nbsp;
    </div>
    <br />
    <asp:TabContainer ID="TabContainer1" runat="server" Width="99%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <asp:TabPanel ID="tb1" runat="server" TabIndex="0" HeaderText="顾客服务通知单">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center">
                        <div style="text-align: center; margin-top: 25px">
                            <h2>
                                顾客服务通知单</h2>
                        </div>
                        <asp:Panel ID="panel" runat="server">
                            <table width="800px" style="margin: auto;">
                                <tr>
                                    <td style="text-align: right; font-size: medium">
                                        文件号：TJZJ-R-M-12
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; font-size: medium">
                                        编号：<asp:TextBox runat="server" ID="CM_BIANHAO" Text="GKFWTZ11001" BorderStyle="None"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                                    </td>
                                </tr>
                            </table>
                            <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto;">
                                <tr>
                                    <td width="100px">
                                        项目名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_PJNAME" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        合同号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_CONTR" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="100px">
                                        设备名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_EQUIP" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        顾客名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CM_CUSNAME" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        信息简介：
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="CM_INFO" runat="server" Width="240px"></asp:TextBox>
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
                                            <asp:CheckBoxList ID="CM_PART" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                CellPadding="5">
                                            </asp:CheckBoxList>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        服务要求：
                                    </td>
                                    <td colspan="5" align="center">
                                        <asp:TextBox ID="CM_ASKFOR" runat="server" Height="100px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                        <asp:FileUpload ID="FileUp" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                            Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                                        <br />
                                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                        <asp:GridView ID="GridView" runat="server" CellPadding="4" CssClass="toptable grid"
                                            AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                            Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="showName" HeaderText="文件名称">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        收货单位：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_DANWEI" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        单位地址：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_DIZHI" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        服务联系人：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_CONN" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        现场联系人：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_LIVE" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        时间要求：
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="CM_ONTIME" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <p>
                                            注：1.非到现场服务的，服务过程及结果填写《顾客服务处理表》送市场部。</p>
                                        <p>
                                            2.到现场服务的，填写《顾客服务处理表》及技术指导方案及要求送市场部。</p>
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
                                            runat="server" />&nbsp;&nbsp;
                                        <asp:HyperLink ID="bmSelect" runat="server" CssClass="hand" onClick="SelPersons();setValue();"
                                            Visible="false">
                                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            选择
                                        </asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp; 审批日期:&nbsp;&nbsp;<asp:Label ID="bmsprq" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <%--  <asp:Panel ID="ldPanel" runat="server">
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
                                            runat="server" />&nbsp;&nbsp;
                                        <asp:HyperLink ID="ldSelect" runat="server" CssClass="hand" onClick="SelPersons();setValue1();"
                                            Visible="false">
                                            <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            选择
                                        </asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp; 审批日期:&nbsp;&nbsp;<asp:Label ID="lzsprq" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>--%>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
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
                        <input id="dep" name="dept" value="07">
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
