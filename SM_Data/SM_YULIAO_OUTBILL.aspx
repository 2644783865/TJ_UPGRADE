<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="SM_YULIAO_OUTBILL.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_YULIAO_OUTBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ���ϳ��ⵥ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        //���ȷ��
        function savePick() {
            var r = Save();

            $("#<%=txtGiver.ClientID %>").val(r.st_name);
            $("#<%=hidGiverId.ClientID %>").val(r.st_id);
            $('#win').dialog('close');
        }

        //��������
        function CalWeight(input) {
            var $tr = $(input).parent().parent();
            var outNum = $(input).val();
            var storeNum = $tr.find("input[name*=txtstore]").val();
            var weight = $tr.find("span[name*=lblWeight]").html();
            $tr.find("input[name*=txtOutWeight]").val((outNum / storeNum * weight).toFixed(4));
        }
    
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="�� ��" CssClass="button-outer" OnClick="btnSave_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="�� ��" CausesValidation="false" OnClick="btnReturn_OnClick"
                                    CssClass="button-outer" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr align="center">
                                <td align="center" colspan="5">
                                    <asp:Label ID="lbltitle1" runat="server" Text="�������ϵ�" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    ���ⵥ�ţ�<asp:Label ID="lblOutCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    ����Ա��<asp:Label ID="lblOutDoc" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="lblOutDocID" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="right">
                                    ����Ա��
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtGiver" runat="server" Width="80px"></asp:TextBox>
                                    <input type="hidden" id="hidGiverId" runat="server" />
                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelPersons()">
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        ѡ��
                                    </asp:HyperLink>
                                    <td align="left">
                                        �������ڣ�<asp:Label ID="lblOutDate" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        ����ţ�<asp:TextBox ID="txtTaskId" runat="server"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="txtTaskId" FirstRowSelected="true"
                                            MinimumPrefixLength="1" ServiceMethod="getTaskID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionSetCount="30">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="4" ForeColor="#333333" EmptyDataText="û��������ݣ�">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���ϱ���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarid" runat="server" Text='<%#Eval("Marid")%>'></asp:Label>
                                        <input type="hidden" runat="server" id="hidId" value='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaizhi" runat="server" Text='<%#Eval("CAIZHI")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGuige" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Length" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="���" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Width" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ͼ��" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Tuxing" runat="server" Text='<%#Eval("TUXING")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����(T)" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" name="lblWeight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="�������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtstore" name="txtstore" runat="server" Text='<%#Eval("NUMBER")%>'
                                            BorderStyle="None" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtoutnum" runat="server" onkeyup="CalWeight(this)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��������" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutWeight" name="txtOutWeight" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��ע" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtnote" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>ָ����Ա</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        �����Ų�ѯ��
                    </td>
                    <td>
                        <input id="dep" name="dept" value="05">
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
            ����</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">ȡ��</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
