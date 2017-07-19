<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_Paint_Scheme_Create.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Paint_Scheme_Create" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    油漆涂装细化方案
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        function getClientId() {
            var paraId1 = '<%= Repeater1.ClientID %>'; //注册控件1
            var paraId2 = '<%= txtid.ClientID %>';
            var paraId3 = '<%= istid.ClientID %>';
            return { Id1: paraId1, Id2: paraId2, Id3: paraId3 }; //生成访问器
        }
    </script>

    <script type="text/javascript">
        function autoCode(input) {
            var marid = document.getElementById(input.id).value;
            var marids = marid.split('|');
            if (marids.length > 5) {
                if (marids[0].indexOf('01.15') > -1) {
                    $(input).val(marids[0]);
                    $(input).parent().next().find("input").val(marids[1]);
                    $(input).parent().next().next().next().next().next().find("input").val(marids[8]);
                    Caculate(input);
                }
            }
        }

        function Caculate(input) {
            var pattem = /^[0-9]*(\.\d+)?$/; //数量验证
            var $tr = $(input).parent().parent();
            var mianji = $tr.find("td:eq(5) input").val();
            var botHouDu = $tr.find("td:eq(8) input").val();
            var botTuBulv = $tr.find("td:eq(11) input").val();
            var midHouDu = $tr.find("td:eq(14) input").val();
            var midTuBulv = $tr.find("td:eq(17) input").val();
            var topHouDu = $tr.find("td:eq(20) input").val();
            var topTuBulv = $tr.find("td:eq(23) input").val();
            if (pattem.test(mianji)) {
                if (pattem.test(botHouDu) && pattem.test(botTuBulv) && botTuBulv != 0) {
                    $tr.find("td:eq(9) input").val((1.8 * botHouDu * mianji / botTuBulv / 10).toFixed(3));
                    $tr.find("td:eq(10) input").val((1.8 * botHouDu * mianji / botTuBulv / 10 * 0.3).toFixed(3));
                }
                if (pattem.test(midHouDu) && pattem.test(midTuBulv) && midTuBulv != 0) {
                    $tr.find("td:eq(15) input").val((1.8 * midHouDu * mianji / midTuBulv / 10).toFixed(3));
                    $tr.find("td:eq(16) input").val((1.8 * midHouDu * mianji / midTuBulv / 10 * 0.3).toFixed(3));
                }
                if (pattem.test(topHouDu) && pattem.test(topTuBulv) && topTuBulv != 0) {
                    $tr.find("td:eq(21) input").val((1.8 * topHouDu * mianji / topTuBulv / 10).toFixed(3));
                    $tr.find("td:eq(22) input").val((1.8 * topHouDu * mianji / topTuBulv / 10 * 0.3).toFixed(3));
                }
                //计算总厚度
                if (pattem.test(botHouDu) && pattem.test(midHouDu) && pattem.test(topHouDu)) {
                    if (botHouDu == "") {
                        botHouDu = 0
                    };
                    if (midHouDu == "") {
                        midHouDu = 0
                    };
                    if (topHouDu == "") {
                        topHouDu = 0
                    };
                    $tr.find("td:eq(26) input").val(parseFloat(botHouDu) + parseFloat(midHouDu) + parseFloat(topHouDu));
                }
                else {
                    $tr.find("td:eq(26) input").val("");
                }
            }
        }
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="99%">
                            <tr>
                                <td style="width: 12%">
                                    任务号：
                                    <asp:Label ID="tsaid" runat="server"></asp:Label>
                                </td>
                                <td style="width: 12%">
                                    合同号：
                                    <asp:Label ID="paint_contract" runat="server"></asp:Label>
                                </td>
                                <td style="width: 12%">
                                    项目名称：
                                    <asp:Label ID="lab_proname" runat="server"></asp:Label>
                                </td>
                                <td style="width: 14%">
                                    设备名称：
                                    <asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div>
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过15条！！！" Width="40"
                                Text="插入"  OnClick="btninsert_Click"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Width="40" Text="删除" OnClick="btndelete_Click" />
                        </td>
                        <td style="width: 10%">
                            <asp:HyperLink ID="hylInput" runat="server">
                                <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />导入数据</asp:HyperLink>
                            <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                Position="Left" Enabled="true" runat="server" OffsetX="-50" OffsetY="30" TargetControlID="hylInput"
                                PopupControlID="Panel3">
                            </cc1:PopupControlExtender>
                            <asp:Panel ID="Panel3" runat="server" Style="display: none;">
                                <table width="320px" style='background-color: #f3f3f3; border: #B9D3EE 3px solid;
                                    font-size: 10pt; font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                    <tr>
                                        <td>
                                            导入倍数:
                                            <asp:TextBox ID="txtInputNum" runat="server" Width="40px" onblur='var pattem= /^[0-9]*(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";this.foucs();}'
                                                Text="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtInputPihao" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="PaintPS" FirstRowSelected="true"
                                                ServicePath="~/Ajax.asmx" TargetControlID="txtInputPihao" UseContextKey="True"
                                                CompletionInterval="10">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnInput" runat="server" Text="导 入" OnClick="btnInput_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            油漆品牌：
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSheBei" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Image ID="Image3" ToolTip="返回上一页" CssClass="hand" Height="16" Width="16" runat="server"
                                onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #000000; height: 480px">
                <div class="cpbox4 xscroll">
                    <table id="tab" class="nowrap cptable fullwidth">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" style="background-color: #B9D3EE" id="row">
                                  <td style="width: 30px" rowspan="2">
                                    </td>
                                    <td rowspan="2">
                                        任务号
                                    </td>
                                    <td rowspan="2">
                                        图号
                                    </td>
                                    <td rowspan="2">
                                        部件名称
                                    </td>
                                    <td rowspan="2">
                                        除锈级别
                                    </td>
                                    <td rowspan="2">
                                        涂装面积(m2)
                                    </td>
                                    <td colspan="6">
                                        底漆
                                    </td>
                                    <td colspan="6">
                                        中间漆
                                    </td>
                                    <td colspan="8">
                                        面漆
                                    </td>
                                    <td rowspan="2">
                                        总厚度
                                    </td>
                                    <td rowspan="2">
                                        备注
                                    </td>
                                    <td rowspan="2">
                                        变更备注
                                    </td>
                                </tr>
                                <tr align="center" style="background-color: #B9D3EE" id="Tr1">
                                    <td>
                                        物料编码
                                    </td>
                                    <td>
                                        种类
                                    </td>
                                    <td>
                                        厚度(um)
                                    </td>
                                    <td>
                                        用量(L)
                                    </td>
                                    <td>
                                        稀释剂(L)
                                    </td>
                                    <td>
                                        涂布率(升/m2)
                                    </td>
                                    <td>
                                        物料编码
                                    </td>
                                    <td>
                                        种类
                                    </td>
                                    <td>
                                        厚度(um)
                                    </td>
                                    <td>
                                        用量(L)
                                    </td>
                                    <td>
                                        稀释剂(L)
                                    </td>
                                    <td>
                                        涂布率(升/m2)
                                    </td>
                                    <td>
                                        物料编码
                                    </td>
                                    <td>
                                        种类
                                    </td>
                                    <td>
                                        厚度(um)
                                    </td>
                                    <td>
                                        用量(L)
                                    </td>
                                    <td>
                                        稀释剂(L)
                                    </td>
                                    <td>
                                        涂布率(升/m2)
                                    </td>
                                    <td>
                                        颜色
                                    </td>
                                    <td>
                                        色号
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget">
                                  
                                    <td>
                                        <asp:CheckBox ID="cbx" runat="server" />
                                    </td>
                                    <td>
                                        <input id="txtTaskID" runat="server" title="任务号" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_ENGID") %>' style="border-style: none;
                                            width: 120px" type="text" />
                                    </td>
                                    <td>
                                        <input id="tuhao" runat="server" title="图号" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TUHAO") %>' style="border-style: none;
                                            width: 120px" type="text" />
                                    </td>
                                    <td>
                                        <input id="partName" runat="server" title="部件名称" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_NAME") %>' style="border-style: none;
                                            width: 120px" type="text" />
                                    </td>
                                    <td>
                                        <input id="level" runat="server" title="除锈级别" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_LEVEL") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtMianJi" runat="server" title="涂装面积" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_MIANJI") %>' style="border-style: none;
                                            width: 80px" type="text" onchange="Caculate(this)" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBotMarid" runat="server" ToolTip="物料编码" Text='<%# Eval("PS_BOTMARID") %>'
                                            BorderStyle="None" Width="80px" onchange="autoCode(this)"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionSetCount="15"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                            ServicePath="~/Ajax.asmx" TargetControlID="txtBotMarid" UseContextKey="True"
                                            CompletionInterval="10">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <input id="txtBotShape" runat="server" title="种类" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BOTSHAPE") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtBotHouDu" runat="server" title="厚度(um)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BOTHOUDU") %>' style="border-style: none;
                                            width: 80px" type="text" onchange="Caculate(this)" />
                                    </td>
                                    <td>
                                        <input id="txtBotYongLiang" runat="server" title="用量(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BOTYONGLIANG") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtBotXiShiJi" runat="server" title="稀释剂(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BOTXISHIJI") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtBotTu" runat="server" title="涂布率" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" readonly="readonly" value='<%# Eval("PS_BOTMWEIGHT") %>'
                                            style="border-style: none; width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMidMarid" runat="server" ToolTip="物料编码" Text='<%# Eval("PS_MIDMARID") %>'
                                            BorderStyle="None" Width="80px" onchange="autoCode(this)"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionSetCount="15"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                            ServicePath="~/Ajax.asmx" TargetControlID="txtMidMarid" UseContextKey="True"
                                            CompletionInterval="10">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <input id="txtMidShape" runat="server" title="种类" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_MIDSHAPE") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtMidHouDu" runat="server" title="厚度(um)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_MIDHOUDU") %>' style="border-style: none;
                                            width: 80px" type="text" onchange="Caculate(this)" />
                                    </td>
                                    <td>
                                        <input id="txtMidYongLiang" runat="server" title="用量(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_MIDYONGLIANG") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtMidXiShiJi" runat="server" title="稀释剂(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_MIDXISHIJI") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtMidTu" runat="server" title="涂布率" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" readonly="readonly" value='<%# Eval("PS_MIDMWEIGHT") %>'
                                            style="border-style: none; width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTopMarid" runat="server" ToolTip="物料编码" Text='<%# Eval("PS_TOPMARID") %>'
                                            BorderStyle="None" Width="80px" onchange="autoCode(this)"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionSetCount="15"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                            ServicePath="~/Ajax.asmx" TargetControlID="txtTopMarid" UseContextKey="True"
                                            CompletionInterval="10">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <input id="txtTopShape" runat="server" title="种类" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPSHAPE") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTopHouDu" runat="server" title="厚度(um)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPHOUDU") %>' style="border-style: none;
                                            width: 80px" type="text" onchange="Caculate(this)" />
                                    </td>
                                    <td>
                                        <input id="txtTopYongLiang" runat="server" title="用量(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPYONGLIANG") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTopXiShiJi" runat="server" title="稀释剂(L)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPXISHIJI") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTopTu" runat="server" title="涂布率" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" readonly="readonly" value='<%# Eval("PS_TOPMWEIGHT") %>'
                                            style="border-style: none; width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTopColor" runat="server" title="颜色" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPCOLOR") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTopColorLabel" runat="server" title="色号" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOPCOLORLABEL") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtTotalHouDu" runat="server" title="总厚度(um)" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_TOTALHOUDU") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtBeiZhu" runat="server" title="备注" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BEIZHU") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                    <td>
                                        <input id="txtBGBeiZhu" runat="server" title="备注" onkeydown="grControlFocusWithoutHidddenPanit(this);"
                                            onfocus="this.select();" value='<%# Eval("PS_BGBEIZHU") %>' style="border-style: none;
                                            width: 80px" type="text" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div>
                <input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
            </div>
            </div>
  
</asp:Content>
