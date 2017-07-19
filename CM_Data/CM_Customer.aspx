<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Customer.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客财产台账
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .yello
        {
            background-color: Yellow;
        }
        .red
        {
            background-color: Red;
        }
        .completionListElement
        {
            margin: 0px;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 150px !important;
            background-color: White;
            font-size: small;
        }
        .listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            color: windowtext;
            font-size: small;
        }
        .highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function() {
            sTable();
        });

        function sTable() {
            superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 3,
                onStart: function() {
                },
                onFinish: function() {
                }
            });
        }
    </script>

    <script type="text/javascript">
        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlBz.ClientID%>');
            var acNameClientId = "<%=acName.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box_right">
        <table width="100%">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <tr>
                <td>
                    <asp:Button ID="Show" runat="server" Text="显示全部" OnClick="Show_Click" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lbshow" Text="出入库:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="sf_getin" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddl_place_SelectedIndexChanged">
                        <asp:ListItem Text="未入库" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已入库" Value="1"></asp:ListItem>
                        <asp:ListItem Text="已出库" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RadioButtonList ID="sf_zj" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddl_place_SelectedIndexChanged" Visible="false" >
                        <asp:ListItem Text="我的质检任务" Value="2" ></asp:ListItem>
                        <asp:ListItem Text="未质检" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已质检" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                    类别：
                    <asp:DropDownList ID="ddl_inout" runat="server" OnSelectedIndexChanged="ddl_place_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Selected="True" Text="-请选择-" Value="a"></asp:ListItem>
                        <asp:ListItem Text="厂内" Value="0"></asp:ListItem>
                        <asp:ListItem Text="厂外" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    按放置地点：
                    <asp:DropDownList ID="ddl_place" runat="server" Width="80px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddl_place_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBz" runat="server">
                        <asp:ListItem Value="CM_PJNAME">项目名称</asp:ListItem>
                        <asp:ListItem Value="CM_CONTR">合同号</asp:ListItem>
                        <asp:ListItem Value="CM_COSTERM">业主名称</asp:ListItem>
                        <asp:ListItem Value="CM_PIC">图号</asp:ListItem>
                        <asp:ListItem Value="CM_EQUIP">产品名称</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 42px" valign="middle">
                    <asp:TextBox ID="txtBox" runat="server" onkeydown="return OnTxtPersonInfoKeyDown();"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="txtBox" ServicePath="CM_Customer.asmx"
                        ServiceMethod="GetData" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                        EnableCaching="false" CompletionListCssClass="completionListElement" CompletionListItemCssClass="listItem"
                        CompletionListHighlightedItemCssClass="highlightedListItem">
                    </asp:AutoCompleteExtender>
                    <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnIn" runat="server" Text="确认入库" OnClick="btnIn_Click" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
                </td>
                <td colspan="3" align="right">
                    <asp:HyperLink ID="hpTask" NavigateUrl="CM_CustomerAdd.aspx?action=add" runat="server">
                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />
                        新建顾客财产</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
    <div class="box-wrapper" style="width: 100%">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="height: 450px; overflow: auto; width: 100%">
                        <div class="cpbox xscroll">
                            <table id="tab" align="center" cellpadding="4" cellspacing="1" class="cptable fullwidth"
                                border="1" style="cursor: pointer; width: 2200px">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle headcolor">
                                            <td width="45px">
                                                <strong>序号</strong>
                                            </td>
                                            <%--<td>
                                                <strong>编号</strong>
                                            </td>--%>
                                            <td>
                                                <strong>项目名称</strong>
                                            </td>
                                            <td>
                                                <strong>类别</strong>
                                            </td>
                                            <td>
                                                <strong>合同号</strong>
                                            </td>
                                            <td>
                                                <strong>业主名称</strong>
                                            </td>
                                            <td>
                                                <strong>设备名称</strong>
                                            </td>
                                            <td>
                                                <strong>图号</strong>
                                            </td>
                                            <td>
                                                <strong>数量</strong>
                                            </td>
                                            <td>
                                                <strong>放置地点</strong>
                                            </td>
                                            <td>
                                                <strong>供货商</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                            <td>
                                                <strong>入库库管</strong>
                                            </td>
                                            <td>
                                                <strong>入库日期</strong>
                                            </td>
                                            <td>
                                                <strong>出库数量</strong>
                                            </td>
                                            <td>
                                                <strong>出库</strong>
                                            </td>
                                            <td>
                                                <strong>出库日期</strong>
                                            </td>
                                            <td>
                                                <strong>申请人</strong>
                                            </td>
                                            <td>
                                                <strong>入库单</strong>
                                            </td>
                                            <td>
                                                <strong>质检结果</strong>
                                            </td>
                                            <td>
                                                <strong>是否合格</strong>
                                            </td>
                                            <td width="6px">
                                                <strong>入</strong>
                                            </td>
                                            <td width="6px">
                                                <strong>出</strong>
                                            </td>
                                            <td>
                                                <strong>出库</strong>
                                            </td>
                                            <td>
                                                <strong>修改</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr runat="server" id="row" class="baseGadget" onmouseover="this.className='highlight'"
                                            onmouseout="this.className='baseGadget'" style="height: 21px">
                                            <td>
                                                <%#Eval("ID_Num")%>
                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                                <asp:Label ID="CM_ID" runat="server" Visible="false" Text='<%#Eval("CM_ID")%>'></asp:Label>
                                            </td>
                                            <%--<td>
                                                <%#Eval("CM_BIANHAO")%>
                                            </td>--%>
                                            <td>
                                                <asp:TextBox runat="server" ID="CM_PJNAME" Text='<%#Eval("CM_PJNAME")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Width="120px" Style="background-color: Transparent; text-align: center;"
                                                    ToolTip='<%#Eval("CM_PJNAME")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("INOROUT")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_CONTR")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_COSTERM")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_EQUIP")%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CM_PIC" Text='<%#Eval("CM_PIC")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center;"
                                                    ToolTip='<%#Eval("CM_PIC")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("CM_NUM")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_PLACE")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_APPNAME")%>
                                            </td>
                                            <td>
                                                <%--<asp:HyperLink ID="look_note" runat="server" Style="font-family: @宋体; color: #336600;
                                                    font-weight: normal;" ToolTip='<%#Eval("CM_NOTE")==""?"无备注信息":Eval("CM_NOTE")%>'>
                                                    <asp:Image ID="InfoImage_ps" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    备注
                                                </asp:HyperLink>--%>
                                                <asp:TextBox runat="server" ID="CM_NOTE" TextMode="MultiLine" Text='<%#Eval("CM_NOTE")==""?"无":Eval("CM_NOTE")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("KEEPER")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_INDATE", "{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_OUTNUM")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_OUT")%>
                                            </td>
                                            <td>
                                                <%#Eval("CM_OUTDATE", "{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%#Eval("ST_NAME")%>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:HyperLink ID="InFile" runat="server" Style="font-family: @宋体; color: #336600;
                                                    font-weight: normal;" NavigateUrl='<%#"CM_CustoInList.aspx?CM_ID="+Eval("CM_ID") %>'
                                                    Target="_blank">
                                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    入库单
                                                </asp:HyperLink>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="zhijian" runat="server" Text='<%# Eval("CM_CHECK").ToString()=="1"?"合格":Eval("CM_CHECK").ToString()=="0"?"不合格":"未质检" %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="hege" runat="server" Text='<%# Eval("CM_SFHG").ToString()=="1"?"合格":Eval("CM_SFHG").ToString()=="0"?"不合格":"" %>'></asp:Label>
                                            </td>
                                            <td class='<%#Eval("CM_BTIN")%>'>
                                                <asp:Label ID="btIn" runat="server" Text='<%# Eval("CM_BTIN").ToString()=="yello"?"入":"" %>'
                                                    Width="30px"></asp:Label>
                                            </td>
                                            <td class='<%#Eval("CM_BTOUT")%>'>
                                                <asp:Label ID="btOut" runat="server" Text='<%# Eval("CM_BTOUT").ToString()=="red"?"出":"" %>'
                                                    Width="30px"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:HyperLink ID="EditOut" runat="server" Style="font-family: @宋体; color: #336600;
                                                    font-weight: normal;" NavigateUrl='<%# ShowOut(Eval("CM_ID").ToString()) %>'>
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    出库
                                                </asp:HyperLink>&nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:HyperLink ID="Edit" runat="server" Style="font-family: @宋体; color: #336600;
                                                    font-weight: normal;" NavigateUrl='<%# Edit(Eval("CM_ID").ToString()) %>'>
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    修改
                                                </asp:HyperLink>&nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="25">
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                            没有记录!</asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <asp:Button ID="btn_del" runat="server" Text="删 除" OnClientClick="return confirm('你确定删除吗?');"
                        OnClick="btn_del_Click" />
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Show" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnIn" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_place" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="sf_getin" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="sf_zj" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_inout" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
