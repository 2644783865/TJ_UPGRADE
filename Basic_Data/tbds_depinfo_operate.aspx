<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="tbds_depinfo_operate.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbds_depinfo_operate"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContentTop">
     <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">
        <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg"
            runat="server" /><div class="RightContentTitle">
                修改增加部门信息</div>
    </div>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Laddmessage" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td align="right">
                            <asp:Label ID="fatherdeptl" runat="server" Text="上级部门:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="fatherdept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fatherdept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RadioButtonList ID="Radiogrouportw" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                TextAlign="Left" Visible="False" OnSelectedIndexChanged="Radiogrouportw_SelectedIndexChanged">
                                <asp:ListItem Text="班组/岗位" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="工种" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%-- </td>
                        <td align="left">--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ID：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_CODE" runat="server" Enabled="False"></asp:TextBox><span class="Error">
                                *</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="填写编号"
                                ControlToValidate="DEP_CODE"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_NAME" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必须输入姓名"
                                ControlToValidate="DEP_NAME"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="right">
                            上级部门ID：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_FATHERID" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DEP_FATHERID"
                                ErrorMessage="必填项"></asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="是否叶节点："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="DEP_ISYENODE" runat="server" RepeatDirection="Horizontal"
                                Enabled="False">
                                <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="否" Value="N" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>                        
                    </tr>
                     <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="是否领料："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="DEP_CY" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>                        
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="是否禁用："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="DEP_SFJY" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>                        
                    </tr>
                    <tr id="tr1" runat="server" visible="false">
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="是否班组或分包商："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdbzyn" runat="server" RepeatDirection="Horizontal">                               
                                <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="是班组" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="是分包商" Value="2" ></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>          
                    
                    </tr>
                    
                    <tr>
                        <td align="right">
                            填写日期：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_FILLDATE" runat="server" Enabled="False"></asp:TextBox><span
                                class="Error"> *</span>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            维护人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_MANCLERK" runat="server" Enabled="False"></asp:TextBox><span
                                class="Error"> *</span>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            备注：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DEP_NOTE" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnupdate" runat="server" Text="修改" OnClick="btnupdate_Click" />
                            <asp:Button ID="btnback" runat="server" Text="返回" CausesValidation="false" 
                                onclick="btnback_Click1"/>
                            <%--<asp:LinkButton ID="LinkButton1" runat="server">返回</asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Label ID="lblupdate" runat="server" Text="message"></asp:Label></div>
            </div>
            <!--box-outer END -->
        </div>
        <!--box-wrapper END -->
    </div>
</asp:Content>
