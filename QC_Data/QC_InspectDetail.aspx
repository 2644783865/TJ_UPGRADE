<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_InspectDetail.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_InspectDetail" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    质检明细查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">

        function ShowViewModal(ID) {
            var date = new Date();
            var time = date.getTime();
            var retVal = window.open("QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id=" + ID);
        }
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table>
                    <tr>
                        <td style="width: 100%" colspan="3">
                            质检编号:
                            <asp:TextBox ID="txtCode" runat="server" Width="80px"></asp:TextBox>
                            计划跟踪号:
                            <asp:TextBox ID="txtPTC" runat="server" Width="80px"></asp:TextBox>
                            报检部门:
                            <asp:TextBox ID="txtDep" runat="server" Width="80px"></asp:TextBox>
                            报检人:
                            <asp:TextBox ID="txtBJR" runat="server" Width="80px"></asp:TextBox>
                            需要检测时间从:
                            <asp:TextBox ID="txtStart" runat="server" Width="80px" class="easyui-datebox"></asp:TextBox>
                            到:
                            <asp:TextBox ID="txtEnd" runat="server" Width="80px" class="easyui-datebox"></asp:TextBox>
                            质检人:
                            <asp:TextBox ID="txtZJR" runat="server" Width="80px"></asp:TextBox>
                            质检次数:<asp:TextBox ID="txtCS" runat="server" Width="80px"></asp:TextBox>
                            质检结果:<asp:DropDownList ID="ddlResult" runat="server" OnSelectedIndexChanged="btnQuery_Click"
                                AutoPostBack="true">
                                <asp:ListItem Value="">请选择</asp:ListItem>
                                <asp:ListItem Value="待检">待检</asp:ListItem>
                                <asp:ListItem Value="报废">报废</asp:ListItem>
                                <asp:ListItem Value="整改">整改</asp:ListItem>
                                <asp:ListItem Value="待定">待定</asp:ListItem>
                                <asp:ListItem Value="让步接收">让步接收</asp:ListItem>
                                <asp:ListItem Value="部分合格">部分合格</asp:ListItem>
                                <asp:ListItem Value="合格">合格</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    质检编号
                                </td>
                                <td>
                                    计划跟踪号
                                </td>
                                <td>
                                    报检部门
                                </td>
                                <td>
                                    报检人
                                </td>
                                <td>
                                    需要检测时间
                                </td>
                                <td>
                                    质检时间
                                </td>
                                <td>
                                    质检人
                                </td>
                                <td>
                                    物料名称
                                </td>
                                <td>
                                    物料编码
                                </td>
                                <td>
                                    规格
                                </td>
                                <td>
                                    材质
                                </td>
                                <td>
                                    检测结果
                                </td>
                                <td>
                                    质检次数
                                </td>
                            </tr>
                            <%-- AFI_ID,PTC,AFI_TSDEP,AFI_MANNM,AFI_RQSTCDATE,AFI_ENDDATE,AFI_QCMANNM,PARTNM,Marid,GUIGE,CAIZHI,AFI_ENDRESLUT,AFI_ASSGSTATE--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td1" runat="server" align="center">
                                    <asp:HyperLink ID="hplId" NavigateUrl='<%#"QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id="+Eval("AFI_ID") %>' Target="_blank" runat="server">
                                    <%#Eval("AFI_ID")%>
                                    </asp:HyperLink>
                                </td>
                                <td id="Td13" runat="server" align="center">
                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("PTC")%>' name="lblBl"></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("AFI_TSDEP")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("AFI_MANNM")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="lblPosition" runat="server" Text='<%#Eval("AFI_RQSTCDATE")%>'></asp:Label>
                                </td>
                                <td id="Td12" runat="server" align="center">
                                    <asp:Label ID="lblSquence" runat="server" Text='<%#Eval("AFI_ENDDATE")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="lblWorkNum" runat="server" Text='<%#Eval("AFI_QCMANNM")%>'></asp:Label>
                                </td>
                                <td id="Td6" runat="server" align="center">
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("PARTNM")%>'></asp:Label>
                                </td>
                                <td id="Td7" runat="server" align="center">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Marid")%>'></asp:Label>
                                </td>
                                <td id="Td8" runat="server" align="center">
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>
                                </td>
                                <td id="Td9" align="center" runat="server">
                                    <asp:Label ID="lblScoreZong" runat="server" Text='<%#Eval("CAIZHI")%>' name="lblScoreZong"></asp:Label>
                                </td>
                                <td id="Td10" runat="server" align="center">
                                    <asp:Label ID="lblBl" runat="server" Text='<%#Eval("RESULT")%>' name="lblBl"></asp:Label>
                                </td>
                                <td id="Td11" runat="server" align="center">
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("AFI_ASSGSTATE")%>' name="lblBl"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
