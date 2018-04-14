<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="tbcs_cusup_add_delete_old.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbcs_cusup_add_delete_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <table width="100%">
        <tr>
            <td>
                <p>
                    厂商信息添加/删除</p>
            </td>
        </tr>
    </table>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnLoad" runat="server" Text="提 交" OnClick="btnLoad_Click" CssClass="button-outer" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="False" CssClass="button-outer"
                                            OnClick="btnReturn_Click" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                    ActiveTabIndex="0">
                    <cc1:TabPanel ID="Tab_INFO" runat="server" Width="100%" HeaderText="厂商信息" TabIndex="0">
                        <ContentTemplate>
                            <asp:Panel ID="Pal_info" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="厂商信息"></asp:Label>(带<span class="Error">*</span>号的为必填项)
                                <div class="box-wrapper">
                                    <div class="box-outer">
                                        <%--<asp:Panel ID="Pal_CODE" runat="server">
                                            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                                <tr>
                                                    <td width="135px">
                                                        公司编号
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCS_CODE" runat="server" Enabled="False" Width="246px"></asp:TextBox>
                                                        <span class="Error">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                            runat="server" ErrorMessage="公司编号不能为空！" ControlToValidate="txtCS_CODE" Display="Dynamic"></asp:RequiredFieldValidator><asp:Button
                                                                ID="btnCreatID" runat="server" Text="生成公司编号" CausesValidation="False" OnClick="btnCreatID_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>--%>
                                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                            <tr>
                                                <td width="135px">
                                                    公司名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_NAME" runat="server" Width="246px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCS_CODE" runat="server" Visible="false" Width="246px"></asp:TextBox>
                                                    <span class="Error">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                        runat="server" ErrorMessage="请输入公司名称！" ControlToValidate="txtCS_NAME"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    所在地
                                                </td>
                                                <td>
                                                    <p>
                                                        <asp:TextBox ID="txtCS_LOCATION" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                            class="Error">*</span>
<%--                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                ErrorMessage="请选择所在地！" ControlToValidate="txtCS_LOCATION"></asp:RequiredFieldValidator>--%>
                                                        <asp:Panel ID="palLocation" runat="server">
                                                            <asp:DropDownList ID="dopCL_LOCATION" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCL_LOCATION_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;省
                                                            <asp:DropDownList ID="dopCL_LOCATION_NEXT" runat="server" AutoPostBack="True" OnTextChanged="dopCL_LOCATION_NEXT_TextChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;（市/区）</asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    助记码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_HRCODE" runat="server" Width="246px"></asp:TextBox><span class="Error">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请填写助记码！"
                                                        ControlToValidate="txtCS_HRCODE"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    所属类型
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_TYPE" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                        class="Error">*[必选项]</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                    <asp:DropDownList ID="dopCS_TYPE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCS_TYPE_SelectedIndexChanged">
                                                        <asp:ListItem Text="-未选择-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="客户" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="采购供应商" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="运输公司" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="技术外协分包商" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="生产外协分包商" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="原材料销售供应商" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="其它" Value="7"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    通讯地址
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_ADDRESS" runat="server" Width="246px"></asp:TextBox><span
                                                        class="Error"> *</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCS_ADDRESS"
                                                        ErrorMessage="请输入通讯地址！" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    公司联系电话
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_PHONO" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    公司联系人姓名
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_CONNAME" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    公司邮箱
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_MAIL" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    邮编
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_ZIP" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    传真
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_FAX" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    产品所属类型
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_COREBS" runat="server" Height="49px" TextMode="MultiLine"
                                                        Width="246px"></asp:TextBox><span class="Error"> *</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填项！"
                                                        ControlToValidate="txtCS_COREBS"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    所供产品
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TB_Scope" runat="server" Height="49px" TextMode="MultiLine" Width="246px"></asp:TextBox>
                                                    <span class="Error">所供产品之间请用中文逗号“，”隔开</span>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    重要等级
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_RANK" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                        class="Error">*[必选项]</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="dopCS_RANK" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCS_RANK_SelectedIndexChanged">
                                                        <asp:ListItem Text="-未选择-" Value="-未选择-"></asp:ListItem>
                                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    开户行：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_BANK" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    开户行帐号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_ACCOUNT" runat="server" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    税号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_TAX" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    申请人
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_MANCLERK" runat="server" Enabled="False" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    申请日期
                                                </td>
                                                <td>
                                                    <input id="txtCS_FILLDATE" type="text" runat="server" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    备注
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_NOTE" runat="server" TextMode="MultiLine" Height="49px" Width="246px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Tab_Review" runat="server" HeaderText="评审信息" TabIndex="1">
                        <ContentTemplate>
                            <asp:Panel ID="Pal_Review" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            指定评审人员
                                        </td>
                                    </tr>
                                </table>
                                <div class="box-wrapper">
                                    <div class="box-outer">
                                        <asp:Panel ID="Pal_person" runat="server">
                                            <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                                border="1">
                                                <tr>
                                                    <td style="width: 130px">
                                                        一级审批：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_oneBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_oneBM_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        &nbsp; 审批人：<asp:DropDownList ID="ddl_onePer" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        二级审批：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_two" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        三级审批：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_three" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Pal_Result" runat="server">
                                            <table id="tab" cellpadding="2" cellspacing="1" class="toptable grid" border="1"
                                                width="100%">
                                                <asp:Panel ID="YIJI" runat="server">
                                                    <tr align="left">
                                                        <td style="width: 135px">
                                                            提出部门：
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lbl_tcbm" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td>
                                                            一级评审意见（部门）
                                                        </td>
                                                        <td colspan="2">
                                                            <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <span>意见：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadioTY1" runat="server" Text="同意" GroupName="select1" AutoPostBack="true"
                                                                            OnCheckedChanged="BMYJ_CheckedChanged" />
                                                                        <asp:RadioButton ID="RadioJJ1" runat="server" Text="拒绝" GroupName="select1" AutoPostBack="true"
                                                                            OnCheckedChanged="BMYJ_CheckedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>备注：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBZ1" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>审核人：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHR1" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <span>审核日期：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHRQ1" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="ERJI" runat="server">
                                                    <tr align="left">
                                                        <td>
                                                            二级评审意见（领导）
                                                        </td>
                                                        <td colspan="2">
                                                            <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <span>意见：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadioTY2" runat="server" Text="同意" GroupName="select2" AutoPostBack="true"
                                                                            OnCheckedChanged="EJYJ_CheckedChanged" />
                                                                        <asp:RadioButton ID="RadioJJ2" runat="server" Text="拒绝" GroupName="select2" AutoPostBack="true"
                                                                            OnCheckedChanged="EJYJ_CheckedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>备注：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBZ2" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>审核人：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHR2" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <span>审核日期：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHRQ2" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="SANJI" runat="server">
                                                    <tr>
                                                        <td>
                                                            三级评审意见（领导）
                                                        </td>
                                                        <td colspan="2">
                                                            <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <span>意见：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadioTY3" runat="server" Text="同意" GroupName="select3" AutoPostBack="true"
                                                                            OnCheckedChanged="SJYJ_CheckedChanged" />
                                                                        <asp:RadioButton ID="RadioJJ3" runat="server" Text="拒绝" GroupName="select3" AutoPostBack="true"
                                                                            OnCheckedChanged="SJYJ_CheckedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>备注：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBZ3" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span>审核人：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHR3" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <span>审核日期：</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextSHRQ3" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
            <td style="width: 150px">
                附件：
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Label ID="lblpathName" runat="server" Visible="false"></asp:Label>
                <asp:Button ID="bntupload" runat="server" Text="上 传" OnClick="bntupload_Click" CausesValidation="False" />
                <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5"
                    CssClass="toptable grid" CellPadding="4" DataKeyNames="ID" ForeColor="#333333"
                    Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("ID") %>' ID="lbid"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FILENAME" HeaderText="文件名称" ItemStyle-Width="60%">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UPLOADDATE" HeaderText="上传时间">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                    OnClick="imgbtndelete_Click" CausesValidation="False" ToolTip="删除" Width="15px"
                                    Height="15px" />
                            </ItemTemplate>
                            <ControlStyle Font-Size="Small" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下载">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtndownload" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                    OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Width="15px"
                                    Height="15px" />
                            </ItemTemplate>
                            <ControlStyle Font-Size="Small" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                        Height="10px" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table id="shanchu_beizhu_table" runat="server" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" visible="false">
            <tr>
                <td style="width: 150px">
                    删除原因
                </td>
                <td style="width: 150px">
                     <asp:TextBox ID="shanchu_beizhu" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>          
                </td>
            </tr>
    </table>
</asp:Content>
