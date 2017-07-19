<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarAdd.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <%-- <script language="javascript" type="text/javascript">
    function add1()
    {
    debugger;
    var carnum=document.getElementById("txtCarNum").Value;
        var sRet = window.showModalDialog('OM_CarAddSafe.aspx?action=add&flag=carnum','obj','dialogWidth=900px;dialogHeight=600px');
        if (sRet == "refresh") 
        {
            window.location.href = window.location.href;
        }
    }
    function add2() {
    var carnum=document.getElementById("txtCarNum").Value;
        var sRet = window.showModalDialog('OM_CarAddOil.aspx?action=add&flag=carnum', 'obj', 'dialogWidth=900px;dialogHeight=600px');
        if (sRet == "refresh") {
            window.location.href = window.location.href;
        }
    }
    </script>--%>
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <table width="100%" class="toptable grid" border="1">
        <tr style="height: 40px">
            <td style="width: 25%">
                车辆状态：
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Text="正常" Value="正常"></asp:ListItem>
                    <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                    <asp:ListItem Text="报废" Value="报废"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2" align="right">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_OnClick" CausesValidation="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="height: 40px">
            <td style="width: 25%">
                车牌号：
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCarNum" runat="server"></asp:TextBox>
                <%--<asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="300px" Visible="false" ></asp:TextBox>--%>
                <span id="span1" runat="server" class="Error">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写车牌号！"
                    ControlToValidate="txtCarNum" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                车辆类型：
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCarType" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td>
                负责人：
            </td>
            <td>
                <asp:DropDownList ID="ddl_fzr" runat="server" Width="200px">
                </asp:DropDownList>
                <%--<asp:TextBox ID="txtfzr" runat="server"></asp:TextBox>--%>
            </td>
            <td>
                加油卡：
            </td>
            <td>
                <asp:TextBox ID="txtcard" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td>
                载客人数：
            </td>
            <td>
                <asp:TextBox ID="txtCapacity" runat="server"></asp:TextBox>
            </td>
            <td>
                颜色：
            </td>
            <td>
                <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td>
                总里程数(KM)：
            </td>
            <td>
                <asp:TextBox ID="txtMileage" runat="server"></asp:TextBox>
            </td>
            <td>
                总用油量(L)：
            </td>
            <td>
                <asp:TextBox ID="txtOil" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td>
                生产厂商：
            </td>
            <td>
                <asp:TextBox ID="txtManufacturer" runat="server"></asp:TextBox>
            </td>
            <td>
                额定吨数(T)：
            </td>
            <td>
                <asp:TextBox ID="txtTunnage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td>
                购买日期：
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" class="easyui-datebox" data-options="editable:false"></asp:TextBox>
            </td>
            <td>
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td align="right">
                图片上传：
            </td>
            <td class="category">
                <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                    Text="上传图片" OnClick="btnUp_Click" CausesValidation="False" />
                <br />
                <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid"
                    AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                    Width="50%">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="fileName" HeaderText="文件名称" HeaderStyle-Wrap="false">
                            <ControlStyle Font-Size="Small" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间" HeaderStyle-Wrap="false">
                            <ControlStyle Font-Size="Small" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                    Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                    ToolTip="删除" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ControlStyle Font-Size="Small" />
                            <%--<HeaderStyle Width="30px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下载" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                    OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                    ToolTip="下载" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ControlStyle Font-Size="Small" />
                            <%--<HeaderStyle Width="30px" />--%>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                        Height="10px" />
                    <RowStyle BackColor="#EFF3FB" />
                </asp:GridView>
            </td>
            <td align="right">
                车辆状态：
            </td>
            <td>
                <asp:RadioButtonList ID="rblState" runat="server" Enabled="false" RepeatDirection="Horizontal">
                    <asp:ListItem Text="在厂" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="不在厂" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <div style="width: 900px">
        车辆保险：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnadd" runat="server" Text="增 加" OnClick="btnadd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
            ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
        <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <asp:Repeater ID="Det_Repeater" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle headcolor">
                        <td width="50px">
                            <strong>序号</strong>
                        </td>
                        <td>
                            <strong>名称</strong>
                        </td>
                        <td>
                            <strong>开始日期</strong>
                        </td>
                        <td>
                            <strong>结束日期</strong>
                        </td>
                        <td>
                            <strong>缴费金额</strong>
                        </td>
                        <td>
                            <strong>备注</strong>
                        </td>
                        <%-- <td>
                                                <strong>证明人</strong>
                                            </td>--%>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                        <td>
                            <%# Convert.ToInt32(Container.ItemIndex +1) %>
                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                            </asp:CheckBox>
                        </td>
                        <td>
                            <asp:TextBox ID="BXNAME" runat="server" Text='<%# Eval("BXNAME")%>' Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="STARTDATE" runat="server" Text='<%# Eval("STARTDATE")%>' Width="200px"
                                class="easyui-datebox" data-options="editable:false"></asp:TextBox>
                        </td>
                        <td>
                            <%--<input id="da" runat="server" type="text" />--%>
                            <asp:TextBox ID="ENDDATE" runat="server" Text='<%# Eval("ENDDATE")%>' Width="200px"
                                class="easyui-datebox" data-options="editable:false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="BXJE" runat="server" Text='<%# Eval("BXJE")%>' Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="NOTE" runat="server" Text='<%# Eval("NOTE")%>' Width="200px"></asp:TextBox>
                        </td>
                        <%-- <td>
                                                <asp:TextBox ID="ST_INDENTITY" runat="server" Text='<%# Eval("ST_INDENTITY")%>' Width="100px"></asp:TextBox>
                                            </td>--%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
            没有记录!</asp:Panel>
        <br />
        <div>
        </div>
    </div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
</asp:Content>
