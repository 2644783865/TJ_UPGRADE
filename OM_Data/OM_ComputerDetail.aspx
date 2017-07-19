<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_ComputerDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ComputerDetail"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    电子设备报修
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
       

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnAudit" runat="server" Text="提交审批" OnClick="btnAudit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClientClick="return Test();"
            OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_ComputerLIst.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="hidden" id="hidConext" runat="server" />
        <input type="hidden" id="hidState" runat="server" />
        <input type="hidden" id="hidAction" runat="server" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center;">
                        <div style="text-align: center;">
                            <h2>
                                报修申请</h2>
                        </div>
                        <br />
                        <asp:Panel ID="Panel0" runat="server">
                            <table width="1000px">
                                <tr>
                                    <td>
                                        申请单号
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        制单人姓名：
                                    </td>
                                    <td>
                                        <asp:Label ID="lb1" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        所在部门：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDep" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        制单时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" runat="server" class="easyui-datebox" Width="120px" data-options="editable:false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        增加行:<asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"
                                            CssClass="center"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                                        &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                    </td>
                                    <td>
                                        备注：
                                    </td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <div style="text-align: center; overflow: auto; height: 300px">
                                <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <asp:Repeater ID="Det_Repeater" runat="server">
                                        <HeaderTemplate>
                                            <tr class="tableTitle headcolor">
                                                <td align="center">
                                                    <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <strong>维修内容</strong>
                                                </td>
                                                <td align="center">
                                                    <strong>类型</strong>
                                                </td>
                                                <td align="center" style="display: none">
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td style="width: 50px">
                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                    <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                    </asp:CheckBox>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="txt1" runat="server" Width="700px" TextMode="MultiLine" Text='<%#Eval("Name")  %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlType" runat="server" SelectedValue='<%#Eval("Type") %>'>
                                                        <asp:ListItem Text="硬件故障" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="网络故障" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="软件故障" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="硬件更换" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="其他" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none">
                                                    <asp:TextBox ID="txt3" runat="server" name="txt3" Visible="false" Width="200px" Text='<%#Eval("Num") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                            </div>
                        </asp:Panel>
                        <div style="float: left; padding-top: 10px">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审核信息" TabIndex="2">
            <ContentTemplate>
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="不需审核" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box-outer">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Panel runat="server" ID="Panel2">
                            <tr>
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult1" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel3">
                            <tr>
                                <td align="center">
                                    二级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult2" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="second_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div>
        <div>
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
                                <input id="dep" name="dept" value="03">
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
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
