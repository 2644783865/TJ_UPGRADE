<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ChangeView.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ChangeView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    变更信息查看
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tb
        {
            width: 400px;
        }
        .center
        {
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        function autoHeight() {
            var iframe = document.getElementById("filework");
            if (iframe.Document) {//ie自有属性
                iframe.style.height = iframe.Document.documentElement.scrollHeight;
            }
            else if (iframe.contentDocument) {//ie,firefox,chrome,opera,safari
                iframe.height = iframe.contentDocument.body.offsetHeight;
            }
        }

        function returnval() {
            var hid = document.getElementById('<%=Hidden.ClientID %>');
            return hid.value;
        }
        function addEvent(eventName, element, fn) {
            if (element.attachEvent) element.attachEvent("on" + eventName, fn);
            else element.addEventListener(eventName, fn, false);
        }

        window.onload = function() {
            var td = document.getElementById("mytd");
            var id = document.getElementById('<%=Hidden.ClientID %>').value;
            var iframeA = document.createElement('iframe');
            iframeA.setAttribute("src", "download.aspx?action=change&id=" + id);
            iframeA.setAttribute("id", "filework");
            iframeA.setAttribute("width", "100%");
            iframeA.setAttribute("marginwidth", "0");
            iframeA.setAttribute("frameborder", "0");
            iframeA.setAttribute("scrolling", "no");
            addEvent("load", iframeA, autoHeight);
            td.appendChild(iframeA);
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField runat="server" ID="Hidden" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LbtnYes" runat="server" OnClientClick="javascript:return confirm('确认提交吗？');"
                                OnClick="btnYes_Click">
                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="同意并提交" ImageUrl="~/Assets/icons/positive.gif"
                                    Height="18" Width="18" runat="server" />
                                同意
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('确认驳回吗？');"
                                OnClick="btnNO_Click">
                                <asp:Image ID="Image1" Style="cursor: hand" ToolTip="驳回并提交" ImageUrl="~/Assets/icons/delete.gif"
                                    Height="18" Width="18" runat="server" />
                                驳回
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnBack" runat="server" CausesValidation="False" OnClick="btn_back_Click">
                                <asp:Image ID="Image7" Style="cursor: hand" ToolTip="返回" ImageUrl="~/Assets/icons/back.png"
                                    Height="17" Width="17" runat="server" />
                                返回
                            </asp:LinkButton>&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="合同信息" TabIndex="0">
            <HeaderTemplate>
                计划单信息
            </HeaderTemplate>
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer">
                        <div style="width: 85%; margin: 0px auto;">
                            <h2 style="text-align: center; margin-top: 20px">
                                经营计划单
                            </h2>
                            <asp:Panel ID="Panel" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td style="width: 120px">
                                            编号：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_ID" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td style="width: 120px">
                                            文件号：
                                        </td>
                                        <td>
                                            TJZJ-R-M-04
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            订货单位：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_COMP" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            项目名称：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_PROJ" runat="server" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            合同号：
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_CONTR" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            对方合同号
                                        </td>
                                        <td>
                                            <asp:Label ID="CM_BGYZHTH" runat="server" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="CM_BGBZ" runat="server" TextMode="MultiLine" Width="600px" Height="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%">
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                                <asp:HiddenField ID="hide" runat="server" Value='<%#Eval("CM_ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_ID" Text='<%# Eval("TSA_ID") %>' Width="150px"
                                                                    CssClass="center"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="产品名称" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_ENGNAME" Text='<%# Eval("TSA_ENGNAME") %>' Width="120px"
                                                                    CssClass="center"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_MAP" Width="100px" Text='<%# Eval("TSA_MAP") %>'
                                                                    CssClass="center"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_NUMBER" Width="50px" Text='<%# Eval("TSA_NUMBER") %>'
                                                                    CssClass="center"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_UNIT" Width="50px" Text='<%# Eval("TSA_UNIT") %>'
                                                                    CssClass="center"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_MATERIAL" Text='<%# Eval("TSA_MATERIAL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_IDNOTE" Text='<%# Eval("TSA_IDNOTE") %>' Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="设备类型" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="TSA_TYPENAME" Text='<%# Eval("TSA_TYPENAME") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="white-space: normal">
                                    <tr>
                                        <td>
                                            <div style="width: 130px">
                                                交货日期：</div>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_FHDATE" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            质量标准：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_LEVEL" Text="符合相关的国家标准或行业标准" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            质量校验与验收：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_TEST" Text="按照图纸或相关国家行业标准校验、验收" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            交货地点及运输方式：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_JHADDRESS" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            包装要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_BZ" Text="符合国家或行业相关包装标准" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            交货要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_JH" runat="server" Text="提供与本合同货物相关的产品合格证书、装箱清单" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            油漆要求：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_YQ" runat="server" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            买方责任人：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_DUTY" runat="server" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_NOTE" Text="后附《工作联系单》" CssClass="tb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            附件上传：
                                        </td>
                                        <td style="text-align: center;">
                                            <div id="mytd">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            变更内容：
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="CM_CHANGE" runat="server" CssClass="tb"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trview" runat="server">
                                        <td>
                                            变更项：
                                        </td>
                                        <td align="center">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="变更" DataField="CHANITEM" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="任务号" DataField="TSA_ID" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="产品名称" DataField="TSA_ENGNAME" HeaderStyle-Wrap="false"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="图号" DataField="TSA_MAP" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="数量" DataField="TSA_NUMBER" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="单位" DataField="TSA_UNIT" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="材质" DataField="TSA_MATERIAL" HeaderStyle-Wrap="false"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="备注" DataField="TSA_IDNOTE" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="200px" />
                                                    <asp:BoundField HeaderText="设备类型" DataField="TSA_TYPENAME" HeaderStyle-Wrap="false"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" />
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="变更" DataField="CHANITEM" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="变更项目" DataField="CM_BGXM" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField HeaderText="变更内容" DataField="CM_CONTENT" HeaderStyle-Wrap="false"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="Pan_ShenHe" HeaderText="评审信息" TabIndex="1">
            <ContentTemplate>
                <div style="border: 1px solid #000000;">
                    <asp:Panel ID="Pan1" runat="server">
                        <table width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                            <tr>
                                <td width="110px" height="25" align="center">
                                    评审信息：
                                </td>
                                <td class="category">
                                    <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                        border="1">
                                        <tr>
                                            <td>
                                                制单人：
                                            </td>
                                            <td>
                                                <asp:Label ID="lb_Zdr" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                制单人意见：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_zdryj" Columns="100" Rows="4" runat="server" TextMode="MultiLine"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
