<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_DriverADD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_DriverADD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    司机档案
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
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();

            $("#<%=txtDriverName.ClientID %>").val(r.st_name);
            $("#<%=hidDriverId.ClientID %>").val(r.st_id);
        
            $('#win').dialog('close');
        }
       

    </script>

    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <table width="100%" class="toptable grid" border="1">
        <tr style="height: 40px">
           
            <td style="width: 25%">
                姓名：
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDriverName" runat="server"></asp:TextBox>
                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelPersons()">
                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    选择
                </asp:HyperLink>
                <input runat="server" type="hidden" id="hidDriverId" />
            </td>
             <td colspan="2" align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_OnClick" />
                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_OnClick" CausesValidation="False" />
            </td>
        </tr>
     <%--   <tr style="height: 40px">
            <td>
                部门：
            </td>
            <td>
                <asp:Label runat="server" ID="lblDep"></asp:Label>
            </td>
            <td>职位：
            </td>
            <td>
              <asp:Label runat="server" ID="lblPos"></asp:Label>
            </td>
        </tr>--%>
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
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div style="width: 900px">
        相关证件：&nbsp;&nbsp;&nbsp;&nbsp;
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
                            <strong>有效期</strong>
                        </td>
                        <td>
                            <strong>下次验证时间</strong>
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
                            <asp:TextBox ID="dNAME" runat="server" Text='<%# Eval("dNAME")%>' Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="dYOUXIAOQI" runat="server" Text='<%# Eval("dYOUXIAOQI")%>' Width="200px"
                                ></asp:TextBox>
                        </td>
                        <td>
                            <%--<input id="da" runat="server" type="text" />--%>
                            <asp:TextBox ID="dENDDATE" runat="server" Text='<%# Eval("dENDDATE")%>' Width="200px"
                                class="easyui-datebox" data-options="editable:false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="dNOTE" runat="server" Text='<%# Eval("dNOTE")%>' Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
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
