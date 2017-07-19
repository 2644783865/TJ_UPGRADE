<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WL_Material_Edit.aspx.cs"
    MasterPageFile="~/masters/BaseMaster.master" Inherits="ZCZJ_DPF.Basic_Data.WL_TYPE_Edit"
    Title="物料信息修改" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">

    <script src="../JS/ChoiceRcmd_TextBox.js" type="text/javascript" charset="GB2312"></script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                编辑物料信息(带<span class="red">*</span>号的为必填项)
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                            <tr id="tr1" runat="server">
                                <td width="25%" height="25" align="right">
                                    ID:
                                </td>
                                <td width="75%" class="category">
                                    <asp:Label ID="m_id" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr id="tr2" runat="server">
                                <td width="25%" height="25" align="right">
                                    类别:
                                </td>
                                <td width="75%" class="category">
                                    <asp:Label ID="m_parentname" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr id="tr3" runat="server">
                                <td width="25%" height="25" align="right">
                                    物料大类：<asp:DropDownList ID="DDLclass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLclass_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="75%" class="category">
                                    物料种类：<asp:DropDownList ID="DDLname" runat="server" BackColor="AntiqueWhite">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" BackColor="AntiqueWhite"
                                        Width="40" Text=""></asp:Label><font color="red">标识文本框为不可修改项,请认真核对！！！</font>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    中文名称:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_name" runat="server"></asp:TextBox><font color="#ff0000"> *</font>
                                    <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" ControlToValidate="m_name"
                                        runat="server" ErrorMessage="中文名称不能为空"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    英文名称:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="txtEngName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    助记码:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_helpcode" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    理论重量:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_meterweight" Text="0" runat="server"></asp:TextBox><font color="#ff0000">
                                        *</font>型材类:kg/m、板材类:kg/m<sup>2</sup>、标准件类:kg/个(台、套等)
                                    <asp:RequiredFieldValidator ID="meterweightRequiredFieldValidator" ControlToValidate="m_meterweight"
                                        runat="server" ErrorMessage="理论重量不能为空"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="meterweightRegularExpressionValidator" runat="server"
                                        ErrorMessage="理论重量输入错误" ControlToValidate="m_meterweight" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$"
                                        SetFocusOnError="True"></asp:RegularExpressionValidator>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <td width="25%" height="25" align="right">
                                米面积:
                            </td>
                            <td width="75%" class="category">
                                <asp:TextBox ID="txtMare" Text="0" runat="server"></asp:TextBox><font color="#ff0000">
                                    *</font>型材类:每米长度的表面积(m<sup>2</sup>)
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMare"
                                    runat="server" ErrorMessage="米面积不能为空"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="米面积输入错误"
                                    ControlToValidate="txtMare" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$"
                                    SetFocusOnError="True"></asp:RegularExpressionValidator>&nbsp;&nbsp;
                            </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    规格:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_standard" Text="" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<%--<font color="#ff0000">
                    *</font>
                <asp:RequiredFieldValidator ID="standardRequiredFieldValidator" ControlToValidate="m_standard" runat="server" ErrorMessage="规格不能为空">
                </asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    材质:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_caizhi" Text="" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    国标:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_guobiao" Text="" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    技术单位:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_TECHUNIT" Text="" onfocus="findChoice(this,1,'div_techunit','ul_techunit');"
                                        runat="server"></asp:TextBox>
                                    <div id="div_techunit" class="hidden" style="position: absolute; background-color: #f3f3f3;
                                        cursor: hand; border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                        <ul id="ul_techunit" style="list-style-type: square; text-align: left; line-height: normal;">
                                        </ul>
                                    </div>
                                    <font color="#ff0000">*</font>&nbsp;&nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="unitRequiredFieldValidator" ControlToValidate="m_TECHUNIT"
                                        runat="server" ErrorMessage="单位不能为空"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    单位转换率:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_CONVERTRATE" Text="" onfocus="findChoice(this,0,'div_convert','ul_convert');"
                                        runat="server"></asp:TextBox>
                                    <div id="div_convert" class="hidden" style="position: absolute; background-color: #f3f3f3;
                                        cursor: hand; border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                        <ul id="ul_convert" style="list-style-type: square; text-align: left; line-height: normal;">
                                        </ul>
                                    </div>
                                    <font color="#ff0000">*</font>&nbsp;技术单位与采购单位间的转换率
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="m_CONVERTRATE"
                                        runat="server" ErrorMessage="单位不能为空"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    物料采购单位:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_PURCUNIT" Text="" onfocus="findChoice(this,2,'div_prucunit','ul_prucunit');"
                                        runat="server"></asp:TextBox>
                                    <div id="div_prucunit" class="hidden" style="position: absolute; background-color: #f3f3f3;
                                        cursor: hand; border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                        <ul id="ul_prucunit" style="list-style-type: square; text-align: left; line-height: normal;">
                                        </ul>
                                    </div>
                                    <font color="#ff0000">*</font>&nbsp;&nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="m_PURCUNIT"
                                        runat="server" ErrorMessage="单位不能为空"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    物料辅助单位:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_fzunit" onfocus="findChoice(this,3,'div_fzunit','ul_fzunit');"
                                        Text="T" runat="server"></asp:TextBox><%--<font color="#ff0000">
                    *</font>--%>
                                    <div id="div_fzunit" class="hidden" style="position: absolute; background-color: #f3f3f3;
                                        cursor: hand; border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                        <ul id="ul_fzunit" style="list-style-type: square; text-align: left; line-height: normal;">
                                        </ul>
                                    </div>
                                    &nbsp;&nbsp;&nbsp;可以为空，不能与采购单位相同
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3"  ControlToValidate="m_fzunit" runat="server" ErrorMessage="单位不能为空"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    最近维护人:
                                </td>
                                <td width="75%" class="category">
                                    <asp:Label ID="lblPerson" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    维护时间:
                                </td>
                                <td width="75%" class="category">
                                    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    状态:
                                </td>
                                <td width="75%" class="category">
                                    <asp:RadioButtonList ID="m_status" RepeatColumns="2" runat="server">
                                        <asp:ListItem Value="0">停用</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">在用</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25" align="right">
                                    备注:
                                </td>
                                <td width="75%" class="category">
                                    <asp:TextBox ID="m_comment" Text="" runat="server" TextMode="MultiLine" Height="45px"
                                        Width="335px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnConfirm" runat="server" Text="确定" OnClick="btnConfirm_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" OnClick="btnCancel_Click" />
                            </tr>
                        </table>
                    </div>
                    <!--box-outer END -->
                </div>
                <!--box-wrapper END -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
