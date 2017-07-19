<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="tbds_locinfo_detail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbds_locinfo_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    基础数据管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript" charset="GB2312">
function Check() 
{
	var arrControls = new Array();
	var arrMessages = new Array();
	
	//获取控件ID
	arrControls.push("<%=txtID.ClientID%>")
	arrControls.push("<%=txtLocname.ClientID%>");


    //提示信息
    arrMessages.push("请填写编码");
    arrMessages.push("请填写地区名称");
	if (!CheckInput(arrControls,arrMessages)) 
	{
	    return false;
	}
	return true;
}
    </script>

    <div class="RightContent">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
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
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>--%>
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td class="right">
                            <asp:Label ID="lblfatherName" runat="server" Text="上级地区:"></asp:Label>
                        </td>
                        <td class="left">
                            <asp:DropDownList ID="ddlfatherName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fatherlocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <%--<td class="tdleft">一级地区名称：</td>
                    <td class="tdright"><asp:TextBox ID="txtLocname1" runat="server" Text="" Width="200"></asp:TextBox>
                    <font color="#ff0000">*</font>
                    </td> --%>
                    </tr>
                    <tr>
                        <td class="right">
                            ID：
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtID" runat="server" Text="" Width="200"></asp:TextBox>
                            <font color="#ff0000">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="right">
                            地区名称：
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtLocname" runat="server" Text="" Width="200"></asp:TextBox>
                            <font color="#ff0000">*</font>
                        </td>
                        <%-- <td class="tdleft">二级地区名称：</td>
                    <td class="tdright"><asp:TextBox ID="txtLocname2" runat="server" Text="" Width="200"></asp:TextBox>
                    </td> --%>
                    </tr>
                    <tr>
                        <td class="right">
                            维护人：
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtName" runat="server" Text="" Width="200" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="right">
                            日期：
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtstarttime" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="right">
                            备注：
                        </td>
                        <td class="left">
                            <asp:TextBox ID="txtNote" runat="server" Text="" TextMode="MultiLine" Rows="3" Width="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnConfirm" runat="server" Text="" OnClick="btnConfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="  取 消  " CausesValidation="false"
                                OnClick="btnCancel_Click" />
                            <asp:Label ID="message" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <%--</ContentTemplate> 
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlfatherName" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel> --%>
            </div>
        </div>
    </div>
</asp:Content>
