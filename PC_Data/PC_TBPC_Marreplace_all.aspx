<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_all.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_all"
    Title="物料代用管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
      
    function bg(temp)
    {
       var i=window.showModalDialog("PC_TBPC_Purchange_all_detail.aspx?maiinfo="+temp,'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no"); 
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <%-- <asp:Button ID="Button1" runat="server" Text="关闭" />&nbsp;&nbsp;
                                <asp:Button ID="Button3" runat="server" Text="新增" />&nbsp;&nbsp;--%>
                                <%--<asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />&nbsp;--%>
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="overflow: hidden; width: 100%">
                    <div class="cpbox xscroll">
                        <div>
                            <table width="100%">
                                <tr>
                                </tr>
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        代用信息
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;申&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请&nbsp;&nbsp;&nbsp;&nbsp;人:
                                        <asp:TextBox ID="tb_peoname" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_peoid" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;申&nbsp;&nbsp;请&nbsp;&nbsp;日&nbsp;&nbsp;期:<asp:TextBox ID="Tb_shijian"
                                            runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;代&nbsp;&nbsp;用&nbsp;&nbsp;批&nbsp;&nbsp;号:<asp:TextBox ID="TextBox_pid"
                                            runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;生&nbsp;&nbsp;产&nbsp;&nbsp;制&nbsp;&nbsp;号:
                                        <asp:TextBox ID="tb_zh" runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目:<asp:TextBox
                                            ID="tb_pjinfo" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;程:<asp:TextBox
                                            ID="tb_enginfo" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="tbpc_marrepallRepeater" runat="server" OnItemDataBound="tbpc_marrepallRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>计划跟踪号</strong>
                                        </td>
                                        <td>
                                            <strong>原材料编码</strong>
                                        </td>
                                        <td>
                                            <strong>原材料名称</strong>
                                        </td>
                                        <td>
                                            <strong>原材料规格</strong>
                                        </td>
                                        <td>
                                            <strong>原材料材质</strong>
                                        </td>
                                        <td>
                                            <strong>原材料国标</strong>
                                        </td>
                                       
                                        <td>
                                            <strong>原材料数量</strong>
                                        </td>
                                         <td>
                                            <strong>原材料单位</strong>
                                        </td>
                                        <td runat="server" visible="false">
                                            <strong>原材料辅助数量</strong>
                                        </td>
                                        <td runat="server" visible="false">
                                            <strong>辅助单位</strong>
                                        </td>
                                        <td runat="server" visible="false">
                                            <strong>长度</strong>
                                        </td>
                                        <td runat="server" visible="false">
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>编辑</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="YPTCODE" runat="server" Text='<%#Eval("YPTCODE")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="YMARID" runat="server" Text='<%#Eval("YMARID")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="YMARNAME" runat="server" Text='<%#Eval("YMARNAME")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="YGUIGE" runat="server" Text='<%#Eval("YGUIGE")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="YCAIZHI" runat="server" Text='<%#Eval("YCAIZHI")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="YGUOBIAO" runat="server" Text='<%#Eval("YGUOBIAO")%>'></asp:Label>&nbsp;
                                        </td>
                                         <td>
                                            <asp:Label ID="YNUM" runat="server" Text='<%#Eval("YNUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="YNUNIT" runat="server" Text='<%#Eval("YNUNIT")%>'></asp:Label>&nbsp;
                                        </td>
                                       
                                        <td runat="server" visible="false">
                                            <asp:Label ID="YFZNUM" runat="server" Text='<%#Eval("YFZNUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td runat="server" visible="false">
                                            <asp:Label ID="YFZUNIT" runat="server" Text='<%#Eval("YFZUNIT")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td runat="server" visible="false">
                                            <asp:Label ID="YLENGTH" runat="server" Text='<%#Eval("YLENGTH")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td runat="server" visible="false">
                                            <asp:Label ID="YWIDTH" runat="server" Text='<%#Eval("YWIDTH")%>'></asp:Label>&nbsp;
                                        </td>
                                        <%--<td runat="server" visible="false">
                                            <asp:Button ID="btn_edit" runat="server" Text="请点击" OnClick="btn_edit_Click" BorderStyle="None" />
                                            &nbsp;
                                        </td>--%>
                                        <td>
                                            <asp:HyperLink ID="hyp_edit" runat="server" ForeColor="Red" Target="_blank" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Marreplace_alldetail.aspx?ptcode="+Eval("YPTCODE")%>'>代用编辑</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="13" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
