<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_JHDQX.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_JHDQX" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    经营计划单取消 
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
        .tab
        {
            width: 80%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                $("#<%=txtSPR1.ClientID%>").val("李利恒");
            }
        })

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtSPR1.ClientID %>").val(r.st_name);
            }
            $('#win').dialog('close');
        }
      function quanxuan(obj){
     if(obj.checked){
       $("#tab [id*='cbxXUHAO']").attr("checked",true);
     }
     else{
        $("#tab [id*='cbxXUHAO']").attr("checked",false);
     }
    }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div style="width: 100%;" align="right">
        <asp:Button runat="server" ID="btnSubmit" Text="确定取消" OnClick="btnSubmit_OnClick"
            Width="80px" Height="30px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnBack" Text="返回" OnClick="btnBack_OnClick" Width="80px"
            Height="30px" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" 
        Width="100%">
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
                            <asp:Panel ID="panJBXX" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td style="width: 120px">
                                            编号：
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCM_ID" runat="server" Width="150px"></asp:Label>
                                            <asp:HiddenField ID="hidID" runat="server" />
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
                                            <asp:Label ID="lbCM_COMP" runat="server" Width="150px"></asp:Label>
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
                                            <asp:Label ID="lbCM_CONTR" runat="server" Width="150px"></asp:Label>
                                        </td>
                                        <td>
                                            对方合同号
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCM_DFCONTR" runat="server" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtTSA_NOTE" runat="server" TextMode="MultiLine" Width="600px" Height="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table id="tab" width="100%">
                                <tr>
                                    <asp:CheckBox runat="server" ID="quanxuan" Text="全选" onclick="quanxuan(this)" align="left" />
                                    <td colspan="4" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvMX" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" OnRowDataBound="gvMX_RowDataBound">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="cbxXUHAO" />
                                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                                <asp:HiddenField ID="hide" runat="server" Value='<%#Eval("CM_ID") %>' />
                                                                <asp:HiddenField ID="cancel" runat="server" Value='<%#Eval("TSA_CANCEL")%>' />
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
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="抄送至">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panCS">
                        <table class="tab">
                            <tr>
                                <td>
                                    市场部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList0" RepeatDirection="Horizontal"
                                        RepeatColumns="6">
                                        <asp:ListItem Text="李利恒" Value="47"></asp:ListItem>
                                        <asp:ListItem Text="李玲" Value="86"></asp:ListItem>
                                        <asp:ListItem Text="胡学来" Value="94"></asp:ListItem>
                                        <asp:ListItem Text="刘学" Value="174"></asp:ListItem>
                                        <asp:ListItem Text="余国彦" Value="163"></asp:ListItem>
                                        <asp:ListItem Text="王艳辉" Value="202"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    技术部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList1" RepeatDirection="Horizontal"
                                        RepeatColumns="5">
                                        <asp:ListItem Text="曹卫亮" Value="146"></asp:ListItem>
                                        <asp:ListItem Text="李小婷" Value="67"></asp:ListItem>
                                        <%--<asp:ListItem Text="周庆泉" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="尉静" Value="165"></asp:ListItem>
                                        <asp:ListItem Text="陈永秀" Value="69"></asp:ListItem>--%>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    质量部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList6" RepeatDirection="Horizontal"
                                        RepeatColumns="5">
                                        <asp:ListItem Text="周庆泉" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="尉静" Value="165"></asp:ListItem>
                                        <asp:ListItem Text="陈永秀" Value="69"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    采购部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList2" RepeatDirection="Horizontal"
                                        RepeatColumns="4">
                                        <asp:ListItem Text="高浩" Value="72"></asp:ListItem>
                                        <asp:ListItem Text="李鑫" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="李洪清" Value="52"></asp:ListItem>
                                        <asp:ListItem Text="王佳月" Value="224"></asp:ListItem>
                                        <asp:ListItem Text="王世洪" Value="38"></asp:ListItem>
                                       <%-- <asp:ListItem Text="巩建钢" Value="114"></asp:ListItem>--%>
                                        <asp:ListItem Text="王盛杰" Value="162"></asp:ListItem>
                                        <asp:ListItem Text="刘慧颖" Value="206"></asp:ListItem>
                                        <asp:ListItem Text="朱倩" Value="268"></asp:ListItem>
                                        <asp:ListItem Text="鲁震" Value="271"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    生产管理部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList3" RepeatDirection="Horizontal"
                                        RepeatColumns="4">
                                        <asp:ListItem Text="于来义" Value="95"></asp:ListItem>
                                        <asp:ListItem Text="董治收" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="孙井刚" Value="91"></asp:ListItem>
                                        <asp:ListItem Text="杨富平" Value="71"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    财务部
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList4" RepeatDirection="Horizontal"
                                        RepeatColumns="2">
                                        <asp:ListItem Text="张慧玲" Value="87"></asp:ListItem>
                                        <asp:ListItem Text="李秋英" Value="171"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    公司领导
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="CheckBoxList5" RepeatDirection="Horizontal"
                                        RepeatColumns="2">
                                        <%--<asp:ListItem Text="周文轶" Value="1"></asp:ListItem>--%>
                                        <asp:ListItem Text="王自清" Value="311"></asp:ListItem>
                                        <asp:ListItem Text="王福泉" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="姜中毅" Value="310"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="审批信息">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div3">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr>
                                <td style="height: 30px" width="20%">
                                    制单人：
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtZDR" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td width="20%">
                                    制单时间：
                                </td>
                                <td width="30%">
                                    <asp:Label runat="server" ID="lbZDR_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    制单人建议：<asp:TextBox runat="server" ID="txtZDR_JY" Text="" TextMode="MultiLine" Width="90%"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR1">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="lb3" Text="市场部长审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%">
                                    <asp:TextBox runat="server" ID="txtSPR1" onfocus="this.blur()" Width="70%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblSPR1_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbSPR1_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtSPR1_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
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
                        <input id="dep" name="dept" value="07">
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
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
