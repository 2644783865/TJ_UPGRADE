<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_JXADDSPADD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_JXADDSPADD" Title="人员绩效审批" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
人员绩效审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
              <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td align="left">
                            姓名：
                            <asp:TextBox ID="txtname" runat="server" OnTextChanged="Textname_TextChanged" Text='<%#Eval("gztz_name")%>'
                                   AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtname"
                                UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:Label ID="lbstid" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            部门：<asp:TextBox ID="txtdepartment" runat="server" Width="100px" onfocus="this.blur()" ></asp:TextBox>
                        </td>
                        
                        <td align="left">
                            绩效工资系数：<asp:TextBox ID="txt_jxgzxs" runat="server" Width="100px" ></asp:TextBox>
                        </td>
                        <td align="left">
                            开始年月：
                           <asp:TextBox ID="tb_yearmonth" Width="90px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                                        </asp:CalendarExtender>
                        </td>
                        <td align="left">
                            类型：<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="--请选择--" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="绩效工资系数调整" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="人员绩效减少" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="新增人员绩效" Value="3"></asp:ListItem>
                                  </asp:DropDownList>
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                  </table>
              </div>
              <div>
                 <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td align="center">
                                <asp:Label ID="title" runat="server" Text="内容"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
</asp:Content>
