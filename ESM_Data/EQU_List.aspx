<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="EQU_List.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_List" Title="机械设备清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <title></title>
    <%--<script src="../JS/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function add()
        {
        alert("111");
        var sRet = window.showModalDialog('equipment_operate.aspx?action=add','','dialogWidth=800px;dialogHeight=700px');
        alert(sRet);
        
        if (sRet == "refresh") 
        {
            window.location.href = window.location.href;
        }
        }
       
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0" Height="500px">
            <asp:TabPanel ID="Tab_equipment" runat="server" Width="100%" HeaderText="机械设备清单">
                <HeaderTemplate>
                    机械设备清单
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="使用部门："></asp:Label>
                                <asp:DropDownList ID="ddlpartment" AutoPostBack="true" runat="server"  Width="180px" 
                                    onselectedindexchanged="ddlpartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="label2" runat="server" Text="分类查看："></asp:Label>
                                <asp:DropDownList ID="ddlSearch" AutoPostBack="true"  runat="server" 
                                    onselectedindexchanged="ddlSearch_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="全部"></asp:ListItem>--%>
                                    <asp:ListItem Text="固定资产名称" Value="AName"></asp:ListItem>
                                    <asp:ListItem Text="规格型号" Value="Spec"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="name" runat="server"></asp:TextBox>
                                <asp:Button ID="search" runat="server" Text="查看" OnClick="search_Click" />
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:window:showModalDialog('EQU_operate.aspx?action=add','','dialogWidth=800px dialogHeight=700px');"
                                    runat="server"><img src="../Assets/images/Add_new_img.gif" />添加设备</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <div class="box-wrapper">
                        <div class="box-outer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1" style="cursor: pointer">
                                        <asp:Repeater ID="equipmentRepeater" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle">
                                                    <td style="width: 100px">
                                                        <strong>卡片编号</strong>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <strong>开始使用日期</strong>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <strong>使用年限</strong>
                                                    </td>
                                                    <%--<td>
                                                        <strong>原值</strong>
                                                    </td>--%>
                                                    <td style="width: 80px">
                                                        <strong>固定资产编号</strong>
                                                    </td>
                                                    <td style="width: 70px">
                                                        <strong>净残值率</strong>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <strong>累计折旧</strong>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <strong>使用部门</strong>
                                                    </td>
                                                    <%--<td style="width: 80px">
                                                        <strong>类别名称</strong>
                                                    </td>--%>
                                                    <td style="width: 80px">
                                                        <strong>固定资产名称</strong>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <strong>净值</strong>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <strong>规格型号</strong>
                                                    </td>
                                                    <%--<td style="width: 80px">
                                                        <strong>折旧方法</strong>
                                                    </td>--%>
                                                    <%--<td style="width: 80px">
                                                        <strong>工作量单位</strong>
                                                    </td>--%>
                                                    <%-- <td style="width: 80px">
                                                        <strong>存放地点</strong>
                                                    </td>--%>
                                                    <%--<td style="width: 80px">
                                                        <strong>对应折旧科目</strong>
                                                    </td>--%><td style="width: 80px">
                                                        <strong>修改</strong>
                                                    </td>
                                                    <td style="width: 50px">
                                                        <strong>删除</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.CName='highlight'" onmouseout="this.CName='baseGadget'"
                                                    ondblclick="<%# showYg(Eval("Id").ToString()) %>" title="双击查看详情">
                                                    <asp:Label ID="Id" runat="server" Visible="false" Text='<%#Eval("Id")%>'></asp:Label>
                                                    <td>
                                                        <%#Eval("CNum")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("SDate")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("DMonth") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("ANum") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("ReRate") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("AcDep") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("UsDe") %>&nbsp;
                                                    </td>
                                                    <%--<td>
                                                        <%#Eval("CName") %>&nbsp;
                                                    </td>--%><td>
                                                        <%#Eval("AName") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("NetVal") %>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Spec") %>&nbsp;
                                                    </td>
                                                    <%--<td>
                                                        <%#Eval("Depre") %>&nbsp;
                                                    </td>--%><%--<td>
                                                        <%#Eval("Unit") %>&nbsp;
                                                    </td>--%><%--<td>
                                                        <%#Eval("Stor") %>&nbsp;
                                                    </td>--%><%--<td>
                                                        <%#Eval("CorAccount") %>&nbsp;
                                                    </td>--%>
                                                    <td title="点击修改">
                                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# editYg(Eval("Id").ToString()) %>'
                                                            runat="server">
                                                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                                runat="server" />修改</asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="checkboxstaff" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                        </asp:CheckBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Panel ID="NoDataPane" runat="server">
                                        没有相关信息!</asp:Panel>
                                    </table>
                                    <div class="PageChange">
                                        <asp:Button ID="deletebt" runat="server" Text="删除" OnClick="deletebt_Click" />
                                    </div>
                                    
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                   
                                    </div> </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    </form>
</body>
</html>
