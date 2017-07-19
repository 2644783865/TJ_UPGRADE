<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"
    ValidateRequest="false" AutoEventWireup="true" CodeBehind="CM_CustoInList.aspx.cs"
    Inherits="ZCZJ_DPF.CM_Data.CM_CustoInList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客财产入库单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }

        $(function() {
            $("#content").blur(function() {
                $("#<%=Hid_ZJ.ClientID %>").val($("#content").html());
            });
            
            if ($("#<%=txtDeclare.ClientID %>").val() != "") {
                $("#content").html($("#<%=txtDeclare.ClientID %>").val());
            }

            //alert($("#<%=txtDeclare.ClientID %>").val());
            $("#content").blur(function() {
                $("#<%=txtDeclare.ClientID %>").val($("#content").html());
                //  alert($("#<%=txtDeclare.ClientID %>").val());
            });
        });
    </script>

    <script src="../JS/jmeditor/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="../JS/jmeditor/JMEditor.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <asp:HiddenField runat="server" ID="psr" />
    <asp:HiddenField runat="server" ID="zdr" />
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" Text="提 交" Visible="false" OnClick="btnsubmit_Click" />
        <asp:Button ID="btnedit" runat="server" Text="编 辑" Visible="false" OnClick="btnedit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper1" style="text-align: center">
                <div class="box-outer">
                    <div style="margin-top: 20px; padding-top: 10px">
                        <h2 style="font-size: x-large">
                            顾客财产入库单</h2>
                    </div>
                    <table width="800px" style="margin: auto">
                        <tr>
                            <td style="text-align: right; font-size: small">
                                文件号：TJZJ-R-M-15
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: small">
                                编号：<asp:Label runat="server" ID="CM_BIANHAO" Width="150px" Text="GKFWCL13073"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                                <asp:TextBox runat="server" ID="txtDeclare" Style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto">
                        <asp:Panel ID="panel" runat="server">
                            <tr>
                                <td width="150px">
                                    顾客名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_COSTERM" runat="server"></asp:TextBox>
                                </td>
                                <td width="150px">
                                    合同号：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_CONTR" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    项目名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_PJNAME" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    设备名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_EQUIP" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    供货商名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_APPNAME" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    报检日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_CMDATE" runat="server" onclick="setday(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" height="100px">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; 入库内容：&nbsp;
                                    <asp:TextBox ID="CM_INCONT" runat="server" Width="300px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                    数量：&nbsp;
                                    <asp:TextBox ID="CM_NUM" runat="server" Width="80px"></asp:TextBox>
                                    <br />
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; 放置位置：&nbsp;
                                    <asp:TextBox ID="CM_PLACE" runat="server"></asp:TextBox><br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    制单人意见：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="CM_ZDYJ" TextMode="MultiLine" Width="500px" Height="40px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="CM_SFHG" runat="server">
                                        <asp:ListItem Text="请选择" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                附件信息：
                            </td>
                            <td colspan="3">
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
                                                    <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/download.jpg"
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
                        <asp:Panel runat="server" ID="panel1">
                            <tr>
                                <td>
                                    校验内容：
                                </td>
                                <td colspan="3" align="left">
                                    <asp:CheckBoxList ID="CM_TEST" runat="server" TextAlign="Left" RepeatDirection="Horizontal"
                                        RepeatLayout="Table" Width="300px">
                                        <asp:ListItem Value="0" Text="尺寸检验"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="无损检验"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="其他检验"></asp:ListItem>
                                    </asp:CheckBoxList>
                                    （该栏由质检部确定）
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp; 检验结果描述：（详细描述存在的质量问题）<br />
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <div id="content" contenteditable="true" class="editDemo" style="overflow: scroll;
                                        height: 120px; width: 90%; border: 1px #B3CDE8 solid;" align="left">
                                        <%=declare%>
                                    </div>
                                    <asp:HiddenField runat="server" ID="Hid_ZJ" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    质检员
                                </td>
                                <td>
                                    <asp:TextBox ID="ST_ZJ" runat="server"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdzj" />
                                    <asp:DropDownList ID="CM_CHECK" runat="server">
                                        <asp:ListItem Text="请选择" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    日期
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_ZJDATE" runat="server" class="easyui-datebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    库管员
                                </td>
                                <td>
                                    <asp:TextBox ID="ST_KG" runat="server"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdkg" />
                                    <asp:CheckBox ID="CM_BTIN" runat="server" /><label for="ctl00_PrimaryContent_CM_BTIN">确定入库</label>
                                </td>
                                <td>
                                    日期
                                </td>
                                <td>
                                    <asp:TextBox ID="CM_KGDATE" runat="server" class="easyui-datebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    入库意见
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="CM_RKYJ" TextMode="MultiLine" Width="500px" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                附件信息
                            </td>
                            <td colspan="3">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Button ID="btnFU" runat="server" Text="上传文件" OnClick="btnFU_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError1" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="80%">
                                    <asp:Repeater runat="server" ID="rptGKCC_ZJ">
                                        <HeaderTemplate>
                                            <tr style="background-color: #71C671;">
                                                <td>
                                                    <strong>文件名称</strong>
                                                </td>
                                                <td>
                                                    <strong>文件上传时间</strong>
                                                </td>
                                                <td>
                                                    <strong>删除</strong>
                                                </td>
                                                <td>
                                                    <strong>下载</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_SHOWNAME" Text='<%#Eval("FILE_SHOWNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_UPDATE" Text='<%#Eval("FILE_UPDATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtndelete1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete1_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload1_OnClick" CausesValidation="False" Text="下载">
                                                        <asp:Image runat="server" ID="imgonload" ImageUrl="~/Assets/images/download.jpg" Height="15px"
                                                            Width="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddFU" />
            <asp:PostBackTrigger ControlID="btnFU" />
            <asp:PostBackTrigger ControlID="rptGKCC_ZJ" />
            <asp:PostBackTrigger ControlID="GridView" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
