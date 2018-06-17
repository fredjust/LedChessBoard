<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mnGetRec = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnGetMoves = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lvMoves = New System.Windows.Forms.ListView()
        Me.chCoup = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chWhite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBlack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SauvegarderToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.JeuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.LiScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckMoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lvRec = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ssRec = New System.Windows.Forms.StatusStrip()
        Me.sslbl1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.menuFichier = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuOuvrir = New System.Windows.Forms.ToolStripMenuItem()
        Me.NouveauToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SauvegarderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLigne = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLigne_Derniere = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCouleur = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCouleur_Rouge = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCouleurVerte = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCouleurBlanche = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLigne_Supprimer = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuLigne_depart = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuModifier = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuModifier_Rec = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuCase_Etteindre = New System.Windows.Forms.ToolStripMenuItem()
        Me.OffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCoups = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCoup_Tous = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCoup_Suivant = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCoup_EnPGN = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSerial = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCOM_6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSPEED = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSPEED_1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSPEED_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSPEED_3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSPEED_4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSerialInit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuSerialClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrientationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPrise = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuA1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuA8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuH1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuH8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuFlip = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmenuLigne = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmenuLigne_Couleur = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuLigne_Verte = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuLigne_Blanche = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuLigne_Rouge = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuLigne_Depart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuLigne_Supprimer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuBoard = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmenuBoard_Allumer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenuBoard_Eteindre = New System.Windows.Forms.ToolStripMenuItem()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.TimerCoup = New System.Windows.Forms.Timer(Me.components)
        Me.pbReduire = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TimerLichess = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ssRec.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.cmenuLigne.SuspendLayout()
        Me.cmenuBoard.SuspendLayout()
        CType(Me.pbReduire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnGetRec
        '
        Me.mnGetRec.Name = "mnGetRec"
        Me.mnGetRec.Size = New System.Drawing.Size(183, 22)
        Me.mnGetRec.Text = "GetRec"
        '
        'mnGetMoves
        '
        Me.mnGetMoves.Name = "mnGetMoves"
        Me.mnGetMoves.Size = New System.Drawing.Size(183, 22)
        Me.mnGetMoves.Text = "GetMoves"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem3.Text = "ToolStripMenuItem3"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem4.Text = "ToolStripMenuItem4"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem5.Text = "ToolStripMenuItem5"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(484, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(389, 488)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lvMoves)
        Me.TabPage1.Controls.Add(Me.MenuStrip2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(381, 462)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Formulaire"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lvMoves
        '
        Me.lvMoves.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCoup, Me.chWhite, Me.chBlack})
        Me.lvMoves.Dock = System.Windows.Forms.DockStyle.Left
        Me.lvMoves.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvMoves.FullRowSelect = True
        Me.lvMoves.GridLines = True
        Me.lvMoves.HideSelection = False
        Me.lvMoves.Location = New System.Drawing.Point(3, 27)
        Me.lvMoves.MultiSelect = False
        Me.lvMoves.Name = "lvMoves"
        Me.lvMoves.Size = New System.Drawing.Size(248, 432)
        Me.lvMoves.TabIndex = 14
        Me.lvMoves.UseCompatibleStateImageBehavior = False
        Me.lvMoves.View = System.Windows.Forms.View.Details
        '
        'chCoup
        '
        Me.chCoup.Text = "n°"
        Me.chCoup.Width = 40
        '
        'chWhite
        '
        Me.chWhite.Text = "Blancs"
        Me.chWhite.Width = 100
        '
        'chBlack
        '
        Me.chBlack.Text = "Noirs"
        Me.chBlack.Width = 100
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FichierToolStripMenuItem, Me.JeuToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(375, 24)
        Me.MenuStrip2.TabIndex = 15
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'FichierToolStripMenuItem
        '
        Me.FichierToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SauvegarderToolStripMenuItem1})
        Me.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem"
        Me.FichierToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.FichierToolStripMenuItem.Text = "Fichier"
        '
        'SauvegarderToolStripMenuItem1
        '
        Me.SauvegarderToolStripMenuItem1.Name = "SauvegarderToolStripMenuItem1"
        Me.SauvegarderToolStripMenuItem1.Size = New System.Drawing.Size(139, 22)
        Me.SauvegarderToolStripMenuItem1.Text = "Sauvegarder"
        '
        'JeuToolStripMenuItem
        '
        Me.JeuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditerToolStripMenuItem, Me.ToolStripMenuItem7, Me.LiScreenToolStripMenuItem})
        Me.JeuToolStripMenuItem.Name = "JeuToolStripMenuItem"
        Me.JeuToolStripMenuItem.Size = New System.Drawing.Size(36, 20)
        Me.JeuToolStripMenuItem.Text = "Jeu"
        '
        'EditerToolStripMenuItem
        '
        Me.EditerToolStripMenuItem.Name = "EditerToolStripMenuItem"
        Me.EditerToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EditerToolStripMenuItem.Text = "Editer"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(149, 6)
        '
        'LiScreenToolStripMenuItem
        '
        Me.LiScreenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigToolStripMenuItem, Me.CheckMoveToolStripMenuItem})
        Me.LiScreenToolStripMenuItem.Name = "LiScreenToolStripMenuItem"
        Me.LiScreenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LiScreenToolStripMenuItem.Text = "LiScreen"
        '
        'ConfigToolStripMenuItem
        '
        Me.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem"
        Me.ConfigToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ConfigToolStripMenuItem.Text = "Config"
        '
        'CheckMoveToolStripMenuItem
        '
        Me.CheckMoveToolStripMenuItem.Enabled = False
        Me.CheckMoveToolStripMenuItem.Name = "CheckMoveToolStripMenuItem"
        Me.CheckMoveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CheckMoveToolStripMenuItem.Text = "Check Move"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lvRec)
        Me.TabPage2.Controls.Add(Me.ssRec)
        Me.TabPage2.Controls.Add(Me.MenuStrip1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(381, 462)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Rec"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lvRec
        '
        Me.lvRec.BackColor = System.Drawing.Color.Black
        Me.lvRec.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.lvRec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvRec.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvRec.ForeColor = System.Drawing.Color.White
        Me.lvRec.FullRowSelect = True
        Me.lvRec.GridLines = True
        Me.lvRec.HideSelection = False
        Me.lvRec.Location = New System.Drawing.Point(3, 27)
        Me.lvRec.Name = "lvRec"
        Me.lvRec.Size = New System.Drawing.Size(375, 410)
        Me.lvRec.TabIndex = 0
        Me.lvRec.UseCompatibleStateImageBehavior = False
        Me.lvRec.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "N°"
        Me.ColumnHeader1.Width = 30
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Rec"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Time"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Off"
        Me.ColumnHeader4.Width = 30
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "On"
        Me.ColumnHeader5.Width = 30
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "FEN"
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Nb"
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Move"
        '
        'ssRec
        '
        Me.ssRec.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sslbl1})
        Me.ssRec.Location = New System.Drawing.Point(3, 437)
        Me.ssRec.Name = "ssRec"
        Me.ssRec.Size = New System.Drawing.Size(375, 22)
        Me.ssRec.TabIndex = 0
        Me.ssRec.Text = "StatusStrip1"
        '
        'sslbl1
        '
        Me.sslbl1.Name = "sslbl1"
        Me.sslbl1.Size = New System.Drawing.Size(90, 17)
        Me.sslbl1.Text = "Find Next Move"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFichier, Me.menuLigne, Me.menuCoups, Me.menuSerial, Me.OrientationToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(375, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'menuFichier
        '
        Me.menuFichier.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuOuvrir, Me.NouveauToolStripMenuItem, Me.ToolStripMenuItem1, Me.SauvegarderToolStripMenuItem})
        Me.menuFichier.Name = "menuFichier"
        Me.menuFichier.Size = New System.Drawing.Size(54, 20)
        Me.menuFichier.Text = "Fichier"
        '
        'menuOuvrir
        '
        Me.menuOuvrir.Name = "menuOuvrir"
        Me.menuOuvrir.Size = New System.Drawing.Size(139, 22)
        Me.menuOuvrir.Text = "Ouvrir"
        '
        'NouveauToolStripMenuItem
        '
        Me.NouveauToolStripMenuItem.Name = "NouveauToolStripMenuItem"
        Me.NouveauToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.NouveauToolStripMenuItem.Text = "Nouveau"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(136, 6)
        '
        'SauvegarderToolStripMenuItem
        '
        Me.SauvegarderToolStripMenuItem.Name = "SauvegarderToolStripMenuItem"
        Me.SauvegarderToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.SauvegarderToolStripMenuItem.Text = "Sauvegarder"
        '
        'menuLigne
        '
        Me.menuLigne.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuLigne_Derniere, Me.menuCouleur, Me.menuLigne_Supprimer, Me.menuLigne_depart, Me.MenuModifier})
        Me.menuLigne.Name = "menuLigne"
        Me.menuLigne.Size = New System.Drawing.Size(48, 20)
        Me.menuLigne.Text = "Ligne"
        '
        'menuLigne_Derniere
        '
        Me.menuLigne_Derniere.Name = "menuLigne_Derniere"
        Me.menuLigne_Derniere.Size = New System.Drawing.Size(159, 22)
        Me.menuLigne_Derniere.Text = "Derniere"
        '
        'menuCouleur
        '
        Me.menuCouleur.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuCouleur_Rouge, Me.menuCouleurVerte, Me.menuCouleurBlanche})
        Me.menuCouleur.Name = "menuCouleur"
        Me.menuCouleur.Size = New System.Drawing.Size(159, 22)
        Me.menuCouleur.Text = "Couleur"
        '
        'menuCouleur_Rouge
        '
        Me.menuCouleur_Rouge.Name = "menuCouleur_Rouge"
        Me.menuCouleur_Rouge.Size = New System.Drawing.Size(116, 22)
        Me.menuCouleur_Rouge.Text = "Rouge"
        '
        'menuCouleurVerte
        '
        Me.menuCouleurVerte.Name = "menuCouleurVerte"
        Me.menuCouleurVerte.Size = New System.Drawing.Size(116, 22)
        Me.menuCouleurVerte.Text = "Verte"
        '
        'menuCouleurBlanche
        '
        Me.menuCouleurBlanche.Name = "menuCouleurBlanche"
        Me.menuCouleurBlanche.Size = New System.Drawing.Size(116, 22)
        Me.menuCouleurBlanche.Text = "blanche"
        '
        'menuLigne_Supprimer
        '
        Me.menuLigne_Supprimer.Name = "menuLigne_Supprimer"
        Me.menuLigne_Supprimer.Size = New System.Drawing.Size(159, 22)
        Me.menuLigne_Supprimer.Text = "Supprimer"
        '
        'menuLigne_depart
        '
        Me.menuLigne_depart.Name = "menuLigne_depart"
        Me.menuLigne_depart.Size = New System.Drawing.Size(159, 22)
        Me.menuLigne_depart.Text = "Nouveau départ"
        '
        'MenuModifier
        '
        Me.MenuModifier.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuModifier_Rec, Me.MenuCase_Etteindre, Me.OffToolStripMenuItem})
        Me.MenuModifier.Name = "MenuModifier"
        Me.MenuModifier.Size = New System.Drawing.Size(159, 22)
        Me.MenuModifier.Text = "Modifier"
        '
        'MenuModifier_Rec
        '
        Me.MenuModifier_Rec.Name = "MenuModifier_Rec"
        Me.MenuModifier_Rec.Size = New System.Drawing.Size(93, 22)
        Me.MenuModifier_Rec.Text = "Rec"
        '
        'MenuCase_Etteindre
        '
        Me.MenuCase_Etteindre.Name = "MenuCase_Etteindre"
        Me.MenuCase_Etteindre.Size = New System.Drawing.Size(93, 22)
        Me.MenuCase_Etteindre.Text = "On"
        '
        'OffToolStripMenuItem
        '
        Me.OffToolStripMenuItem.Name = "OffToolStripMenuItem"
        Me.OffToolStripMenuItem.Size = New System.Drawing.Size(93, 22)
        Me.OffToolStripMenuItem.Text = "Off"
        '
        'menuCoups
        '
        Me.menuCoups.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuCoup_Tous, Me.menuCoup_Suivant, Me.menuCoup_EnPGN})
        Me.menuCoups.Name = "menuCoups"
        Me.menuCoups.Size = New System.Drawing.Size(53, 20)
        Me.menuCoups.Text = "Coups"
        '
        'menuCoup_Tous
        '
        Me.menuCoup_Tous.Name = "menuCoup_Tous"
        Me.menuCoup_Tous.Size = New System.Drawing.Size(114, 22)
        Me.menuCoup_Tous.Text = "Tous"
        '
        'menuCoup_Suivant
        '
        Me.menuCoup_Suivant.Name = "menuCoup_Suivant"
        Me.menuCoup_Suivant.Size = New System.Drawing.Size(114, 22)
        Me.menuCoup_Suivant.Text = "Suivant"
        '
        'menuCoup_EnPGN
        '
        Me.menuCoup_EnPGN.Name = "menuCoup_EnPGN"
        Me.menuCoup_EnPGN.Size = New System.Drawing.Size(114, 22)
        Me.menuCoup_EnPGN.Text = "En PGN"
        '
        'menuSerial
        '
        Me.menuSerial.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuCOM, Me.menuSPEED, Me.menuSerialInit, Me.ToolStripMenuItem2, Me.menuSerialClose})
        Me.menuSerial.Name = "menuSerial"
        Me.menuSerial.Size = New System.Drawing.Size(47, 20)
        Me.menuSerial.Text = "Serial"
        '
        'menuCOM
        '
        Me.menuCOM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuCOM_1, Me.menuCOM_2, Me.menuCOM_3, Me.menuCOM_4, Me.menuCOM_5, Me.menuCOM_6})
        Me.menuCOM.Name = "menuCOM"
        Me.menuCOM.Size = New System.Drawing.Size(167, 22)
        Me.menuCOM.Text = "COM"
        '
        'menuCOM_1
        '
        Me.menuCOM_1.CheckOnClick = True
        Me.menuCOM_1.Enabled = False
        Me.menuCOM_1.Name = "menuCOM_1"
        Me.menuCOM_1.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_1.Text = "COM"
        Me.menuCOM_1.Visible = False
        '
        'menuCOM_2
        '
        Me.menuCOM_2.CheckOnClick = True
        Me.menuCOM_2.Enabled = False
        Me.menuCOM_2.Name = "menuCOM_2"
        Me.menuCOM_2.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_2.Text = "COM"
        Me.menuCOM_2.Visible = False
        '
        'menuCOM_3
        '
        Me.menuCOM_3.CheckOnClick = True
        Me.menuCOM_3.Enabled = False
        Me.menuCOM_3.Name = "menuCOM_3"
        Me.menuCOM_3.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_3.Text = "COM"
        Me.menuCOM_3.Visible = False
        '
        'menuCOM_4
        '
        Me.menuCOM_4.CheckOnClick = True
        Me.menuCOM_4.Enabled = False
        Me.menuCOM_4.Name = "menuCOM_4"
        Me.menuCOM_4.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_4.Text = "COM"
        Me.menuCOM_4.Visible = False
        '
        'menuCOM_5
        '
        Me.menuCOM_5.CheckOnClick = True
        Me.menuCOM_5.Enabled = False
        Me.menuCOM_5.Name = "menuCOM_5"
        Me.menuCOM_5.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_5.Text = "COM"
        Me.menuCOM_5.Visible = False
        '
        'menuCOM_6
        '
        Me.menuCOM_6.Name = "menuCOM_6"
        Me.menuCOM_6.Size = New System.Drawing.Size(102, 22)
        Me.menuCOM_6.Text = "Find"
        '
        'menuSPEED
        '
        Me.menuSPEED.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuSPEED_1, Me.menuSPEED_2, Me.menuSPEED_3, Me.menuSPEED_4})
        Me.menuSPEED.Name = "menuSPEED"
        Me.menuSPEED.Size = New System.Drawing.Size(167, 22)
        Me.menuSPEED.Text = "SPEED"
        '
        'menuSPEED_1
        '
        Me.menuSPEED_1.Name = "menuSPEED_1"
        Me.menuSPEED_1.Size = New System.Drawing.Size(110, 22)
        Me.menuSPEED_1.Text = "9600"
        '
        'menuSPEED_2
        '
        Me.menuSPEED_2.Name = "menuSPEED_2"
        Me.menuSPEED_2.Size = New System.Drawing.Size(110, 22)
        Me.menuSPEED_2.Text = "38400"
        '
        'menuSPEED_3
        '
        Me.menuSPEED_3.Checked = True
        Me.menuSPEED_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menuSPEED_3.Name = "menuSPEED_3"
        Me.menuSPEED_3.Size = New System.Drawing.Size(110, 22)
        Me.menuSPEED_3.Text = "57600"
        '
        'menuSPEED_4
        '
        Me.menuSPEED_4.Name = "menuSPEED_4"
        Me.menuSPEED_4.Size = New System.Drawing.Size(110, 22)
        Me.menuSPEED_4.Text = "115200"
        '
        'menuSerialInit
        '
        Me.menuSerialInit.Name = "menuSerialInit"
        Me.menuSerialInit.Size = New System.Drawing.Size(167, 22)
        Me.menuSerialInit.Text = "Init COM6 115200"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(164, 6)
        '
        'menuSerialClose
        '
        Me.menuSerialClose.Enabled = False
        Me.menuSerialClose.Name = "menuSerialClose"
        Me.menuSerialClose.Size = New System.Drawing.Size(167, 22)
        Me.menuSerialClose.Text = "Close"
        '
        'OrientationToolStripMenuItem
        '
        Me.OrientationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuPrise, Me.menuFlip})
        Me.OrientationToolStripMenuItem.Name = "OrientationToolStripMenuItem"
        Me.OrientationToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.OrientationToolStripMenuItem.Text = "Orientation"
        '
        'menuPrise
        '
        Me.menuPrise.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuA1, Me.menuA8, Me.menuH1, Me.menuH8})
        Me.menuPrise.Name = "menuPrise"
        Me.menuPrise.Size = New System.Drawing.Size(133, 22)
        Me.menuPrise.Text = "Prise en H8"
        '
        'menuA1
        '
        Me.menuA1.Name = "menuA1"
        Me.menuA1.Size = New System.Drawing.Size(105, 22)
        Me.menuA1.Text = "en A1"
        '
        'menuA8
        '
        Me.menuA8.Name = "menuA8"
        Me.menuA8.Size = New System.Drawing.Size(105, 22)
        Me.menuA8.Text = "en A8"
        '
        'menuH1
        '
        Me.menuH1.Name = "menuH1"
        Me.menuH1.Size = New System.Drawing.Size(105, 22)
        Me.menuH1.Text = "en H1"
        '
        'menuH8
        '
        Me.menuH8.Checked = True
        Me.menuH8.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menuH8.Name = "menuH8"
        Me.menuH8.Size = New System.Drawing.Size(105, 22)
        Me.menuH8.Text = "en H8"
        '
        'menuFlip
        '
        Me.menuFlip.Name = "menuFlip"
        Me.menuFlip.Size = New System.Drawing.Size(133, 22)
        Me.menuFlip.Text = "Flip V"
        '
        'cmenuLigne
        '
        Me.cmenuLigne.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmenuLigne_Couleur, Me.cmenuLigne_Depart, Me.cmenuLigne_Supprimer})
        Me.cmenuLigne.Name = "cmenuLigne"
        Me.cmenuLigne.Size = New System.Drawing.Size(130, 70)
        '
        'cmenuLigne_Couleur
        '
        Me.cmenuLigne_Couleur.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmenuLigne_Verte, Me.cmenuLigne_Blanche, Me.cmenuLigne_Rouge})
        Me.cmenuLigne_Couleur.Name = "cmenuLigne_Couleur"
        Me.cmenuLigne_Couleur.Size = New System.Drawing.Size(129, 22)
        Me.cmenuLigne_Couleur.Text = "couleur"
        '
        'cmenuLigne_Verte
        '
        Me.cmenuLigne_Verte.Name = "cmenuLigne_Verte"
        Me.cmenuLigne_Verte.Size = New System.Drawing.Size(116, 22)
        Me.cmenuLigne_Verte.Text = "Verte"
        '
        'cmenuLigne_Blanche
        '
        Me.cmenuLigne_Blanche.Name = "cmenuLigne_Blanche"
        Me.cmenuLigne_Blanche.Size = New System.Drawing.Size(116, 22)
        Me.cmenuLigne_Blanche.Text = "Blanche"
        '
        'cmenuLigne_Rouge
        '
        Me.cmenuLigne_Rouge.Name = "cmenuLigne_Rouge"
        Me.cmenuLigne_Rouge.Size = New System.Drawing.Size(116, 22)
        Me.cmenuLigne_Rouge.Text = "Rouge"
        '
        'cmenuLigne_Depart
        '
        Me.cmenuLigne_Depart.Name = "cmenuLigne_Depart"
        Me.cmenuLigne_Depart.Size = New System.Drawing.Size(129, 22)
        Me.cmenuLigne_Depart.Text = "Depart"
        '
        'cmenuLigne_Supprimer
        '
        Me.cmenuLigne_Supprimer.Name = "cmenuLigne_Supprimer"
        Me.cmenuLigne_Supprimer.Size = New System.Drawing.Size(129, 22)
        Me.cmenuLigne_Supprimer.Text = "Supprimer"
        '
        'cmenuBoard
        '
        Me.cmenuBoard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmenuBoard_Allumer, Me.cmenuBoard_Eteindre})
        Me.cmenuBoard.Name = "mnFrm"
        Me.cmenuBoard.Size = New System.Drawing.Size(118, 48)
        '
        'cmenuBoard_Allumer
        '
        Me.cmenuBoard_Allumer.Name = "cmenuBoard_Allumer"
        Me.cmenuBoard_Allumer.Size = New System.Drawing.Size(117, 22)
        Me.cmenuBoard_Allumer.Text = "Allumer"
        '
        'cmenuBoard_Eteindre
        '
        Me.cmenuBoard_Eteindre.Name = "cmenuBoard_Eteindre"
        Me.cmenuBoard_Eteindre.Size = New System.Drawing.Size(117, 22)
        Me.cmenuBoard_Eteindre.Text = "Eteindre"
        '
        'SerialPort1
        '
        Me.SerialPort1.PortName = "COM6"
        '
        'TimerCoup
        '
        Me.TimerCoup.Interval = 1000
        '
        'pbReduire
        '
        Me.pbReduire.BackgroundImage = Global.TestGraphic.My.Resources.Resources.reduire0
        Me.pbReduire.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pbReduire.Location = New System.Drawing.Point(832, 37)
        Me.pbReduire.Name = "pbReduire"
        Me.pbReduire.Size = New System.Drawing.Size(26, 17)
        Me.pbReduire.TabIndex = 12
        Me.pbReduire.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(444, 447)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'TimerLichess
        '
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 507)
        Me.Controls.Add(Me.pbReduire)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(764, 465)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChessboARDuino VB"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ssRec.ResumeLayout(False)
        Me.ssRec.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.cmenuLigne.ResumeLayout(False)
        Me.cmenuBoard.ResumeLayout(False)
        CType(Me.pbReduire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pbReduire As System.Windows.Forms.PictureBox
    Friend WithEvents mnGetRec As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnGetMoves As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lvMoves As System.Windows.Forms.ListView
    Friend WithEvents chCoup As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWhite As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBlack As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvRec As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ssRec As System.Windows.Forms.StatusStrip
    Friend WithEvents sslbl1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents menuFichier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOuvrir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLigne As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLigne_Supprimer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCouleur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCouleur_Rouge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCouleurVerte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCouleurBlanche As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCoups As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCoup_Tous As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCoup_Suivant As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCoup_EnPGN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLigne_depart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmenuLigne_Supprimer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne_Couleur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne_Verte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne_Blanche As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne_Rouge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuLigne_Depart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLigne_Derniere As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuModifier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuModifier_Rec As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuCase_Etteindre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuBoard As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmenuBoard_Allumer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenuBoard_Eteindre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NouveauToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSerial As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSPEED As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSPEED_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSPEED_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSPEED_3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSPEED_4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSerialInit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSerialClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCOM_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SauvegarderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerCoup As System.Windows.Forms.Timer
    Friend WithEvents MenuStrip2 As System.Windows.Forms.MenuStrip
    Friend WithEvents FichierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SauvegarderToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JeuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OrientationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuA1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuA8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuH1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuH8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFlip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LiScreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckMoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerLichess As System.Windows.Forms.Timer

End Class
